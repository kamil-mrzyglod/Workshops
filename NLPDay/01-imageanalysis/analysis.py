# https://docs.microsoft.com/id-id/azure/cognitive-services/computer-vision/concept-tagging-images

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

#url = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Broadway_and_Times_Square_by_night.jpg/450px-Broadway_and_Times_Square_by_night.jpg"
#url = "https://www.spendwithpennies.com/wp-content/uploads/2018/08/SpendWithPennies-Oven-Baked-Chicken-Breast-22.jpg"
url = "https://pixel.nymag.com/imgs/fashion/daily/2019/03/13/13-dangerous-chicken.w700.h700.jpg"
#url = "https://www.wonderopolis.org/wp-content/uploads/2010/11/Wonder-74-City-Planning-Static-Image.jpg"

image_analysis = client.analyze_image(url,visual_features=[VisualFeatureTypes.tags])

for tag in image_analysis.tags:
    print(tag)

# Tags provide a `hint` property, which helps you understand the meaning of the tag
# in case it was ambiguous
# Taging is not limited to the objects which are the main subject - the API
# tags also objects and persons in the background 