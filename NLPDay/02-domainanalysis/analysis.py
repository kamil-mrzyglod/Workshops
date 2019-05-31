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
sys.stdout.write("Initialized successfully!\r\n")
sys.stdin.flush()

models = client.list_models()

for x in models.models_property:
    print(x)

domain = "landmarks"
url = "https://r-scale-bd.dcs.redcdn.pl/scale/o2/tvn/web-content/m/p1/i/c59b469d724f7919b7d35514184fdc0f/d87dc981-ab6e-4861-b150-998aa0d87a6f.jpg?type=1&srcmode=0&srcx=1%2F1&srcy=560%2F1000&srcw=1%2F1&srch=85%2F100&dstw=1260&dsth=708&quality=75"
language = "en"

analysis = client.analyze_image_by_domain(domain, url, language)

for landmark in analysis.result["landmarks"]:
    print(landmark["name"])
    print(landmark["confidence"])

domain = "celebrities"
url = "https://www.wprost.pl/_thumb/0d/1f/6f7638bdc775eeaeccd3f317330d.jpeg"
language = "en"

analysis = client.analyze_image_by_domain(domain, url, language)

for celebrity in analysis.result["celebrities"]:
    print(celebrity["name"])
    print(celebrity["confidence"])