# https://docs.microsoft.com/id-id/azure/cognitive-services/computer-vision/concept-object-detection

import os
import sys

from azure.cognitiveservices.vision.computervision import ComputerVisionClient
from azure.cognitiveservices.vision.computervision.models import VisualFeatureTypes
from msrest.authentication import CognitiveServicesCredentials

# Get endpoint and key from environment variables
endpoint = "https://westeurope.api.cognitive.microsoft.com/"
key = ""

# Set credentials
credentials = CognitiveServicesCredentials(key)

# Create client
client = ComputerVisionClient(endpoint, credentials)

sys.stdout.write("Initialized API version {0} with config {1}\r\n".format(client.api_version, client.config.endpoint))
sys.stdout.write("Initialized successfully!")
sys.stdin.flush()

url = "https://i.wpimg.pl/O/644x427/d.wpimg.pl/1167827718--137188907/palac-kultury-i-nauki.jpg"

image_analysis = client.analyze_image(url,visual_features=[VisualFeatureTypes.description])

print(image_analysis.description)
for caption in image_analysis.description.captions:
    print(caption)