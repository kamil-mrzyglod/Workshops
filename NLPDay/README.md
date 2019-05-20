# NLPDay 
## Working with images in Azure using Vision API
Azure Cognitive Services are a set of advanced APIs, which allow you to quickly build intelligent applications hosted in the cloud. During the workshop, you will learn the basics of this particular service using one of its components - Vision API. We will try to perform simple exercises like face detection, emotion detection and handwriting recognition and compare the result with custom models and services.
### Getting started
Before you get started, run the following command to install Azure Cognitive Services Computer Vision SDK for Python:
```
pip install azure-cognitiveservices-vision-computervision
```
Once the package is installed, go to the `getting-started` directory and run the `gettingstarted.py` file. You will find ACCOUNT_ENDPOINT and ACCOUNT_KEY values, which can be either obtained from your Azure Cognitive Services instance or by getting a free, 7-day trial version: https://azure.microsoft.com/en-us/try/cognitive-services/.

The result of running the script should be similar to the following:
```
python gettingstarted.py
Initialized API version 2.0 with config https://westeurope.api.cognitive.microsoft.com/
Initialized successfully!
```