import boto3
from PIL import Image
import json
import sys
import io

# AWS Textract client setup
# access_key = ''
# secret_access_key = ''

textract_client = boto3.client('textract', aws_access_key_id=access_key, 
                               aws_secret_access_key=secret_access_key, 
                               region_name='us-east-1')

def ocr_image(image_path):
    # Reading the image file
    with Image.open(image_path) as img:
        img_byte_array = io.BytesIO()
        img.save(img_byte_array, format='PNG')
        img_bytes = img_byte_array.getvalue()
        
        response = textract_client.detect_document_text(Document={'Bytes': img_bytes})

    # Extracting text from the response
    extract = []
    for item in response["Blocks"]:
        if item["BlockType"] == "LINE":
            extract.append(item["Text"])

    # Prepare JSON response
    result = {
        'extracted_text': extract,
        'image-path': image_path
    }

    # Return pretty-printed JSON
    return json.dumps(result, indent=4, ensure_ascii=False)

if __name__ == "__main__":
    image_path = sys.argv[1]
    access_key = sys.argv[2]
    secret_access_key = sys.argv[3]
    print(ocr_image(image_path))
