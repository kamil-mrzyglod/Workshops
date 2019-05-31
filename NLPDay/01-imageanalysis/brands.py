# https://docs.microsoft.com/id-id/azure/cognitive-services/computer-vision/concept-brand-detection

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

url = "https://thumbs.img-sprzedajemy.pl/1000x901c/38/82/5e/redbull-kurtka-skorzana-arai-rozmiar-xxl-kurtki-bialystok-455154946.jpg"

image_analysis = client.analyze_image(url,visual_features=[VisualFeatureTypes.brands])

for brand in image_analysis.brands:
    print(brand.name, brand.rectangle)

# Tags provide a `hint` property, which helps you understand the meaning of the tag
# in case it was ambiguous
# Taging is not limited to the objects which are the main subject - the API
# tags also objects and persons in the background 