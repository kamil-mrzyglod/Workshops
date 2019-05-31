# https://docs.microsoft.com/id-id/azure/cognitive-services/computer-vision/concept-detecting-color-schemes

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

url = "https://www.wonderopolis.org/wp-content/uploads/2010/11/Wonder-74-City-Planning-Static-Image.jpg"
url = "https://cdn.natemat.pl/1e8b01bb39c8c37efdf32ce25f41979e,780,0,0,0.jpg"
# url = "http://bi.gazeta.pl/im/5f/c2/15/z22815071IH,Palac-Kultury-i-Nauki-w-Warszawie.jpg"
#url = "https://images.footway.com/01/601/60115-35/front/705/705/60115-35.png?fwv=1.0&auto=format,compress"

image_analysis = client.analyze_image(url,visual_features=[VisualFeatureTypes.color])

print(image_analysis.color)