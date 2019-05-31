import os
import sys
import time

from azure.cognitiveservices.vision.computervision import ComputerVisionClient
from azure.cognitiveservices.vision.computervision.models import VisualFeatureTypes
from azure.cognitiveservices.vision.computervision.models import TextRecognitionMode
from azure.cognitiveservices.vision.computervision.models import TextOperationStatusCodes
from msrest.authentication import CognitiveServicesCredentials

# Get endpoint and key from environment variables
endpoint = "https://westeurope.api.cognitive.microsoft.com/"
key = ""

# Set credentials
credentials = CognitiveServicesCredentials(key)

# Create client
client = ComputerVisionClient(endpoint, credentials)

sys.stdout.write("Initialized API version {0} with config {1}\r\n".format(client.api_version, client.config.endpoint))
sys.stdout.write("Initialized successfully!\r\n")
sys.stdin.flush()

#url = "https://azurecomcdn.azureedge.net/cvt-1979217d3d0d31c5c87cbd991bccfee2d184b55eeb4081200012bdaf6a65601a/images/shared/cognitive-services-demos/read-text/read-1-thumbnail.png"
url = "https://nlpdayeuwstorage.blob.core.windows.net/nlpday/IMG_0093.jpeg"
mode = TextRecognitionMode.handwritten
raw = True
custom_headers = None
numberOfCharsInOperationId = 36

rawHttpResponse = client.batch_read_file(url, mode, custom_headers,  raw)

operationLocation = rawHttpResponse.headers["Operation-Location"]
idLocation = len(operationLocation) - numberOfCharsInOperationId
operationId = operationLocation[idLocation:]

while True:
    result = client.get_read_operation_result(operationId)
    if result.status not in ['NotStarted', 'Running']:
        break
    time.sleep(1)

if result.status == TextOperationStatusCodes.succeeded:
    for textResult in result.recognition_results:
        for line in textResult.lines:
            print(line.text)
            print(line.bounding_box)