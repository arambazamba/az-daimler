## Table Of Contents

1. [What are Azure Cognitive Services?](#what-are-azure-cognitive-services)
1. [Create a Cognitive Service](#create-a-cognitive-service)
1. [Analyze Sentiment](#analyze-sentiment-and-opinions)
1. [(OPTIONAL) Containerize the Cognitive Service](#(optional)-containerize-the-cognitive-service)
1. [(OPTIONAL) Use Azure Text Analytics in a Web Application](#(optional)-use-azure-text-analytics-in-a-web-application)
1. [(OPTIONAL) Use Postman to understand all features](#(optional)-use-postman-to-understand-all-features)
1. [Cleanup](#cleanup)

## What are Azure Cognitive Services?

Azure Cognitive Services:

- are APIs, SDKs and services available to help developers build intelligent applications without having direct Artificial Intelligence (AI), data science skills or knowledge.
- enable developers to easily add cognitive features into their applications.

The goal of Azure Cognitive Services is to help developers create applications that can see, hear, speak, understand and even begin to reason.
These services can be categorized into six main pillars - _Vision_, _Speech_, _Language_, _Web Search_, _Decision_ and _Open AI_.

We offer a separate training that will go into greater depth also covering Azure Machine Learning Services and MLOps - reach out to us if you are interested. Today we will focus on one Feature of the Azure Cognitive Service for Language to consolidate the understanding of these services. The Azure Cognitive Service for Language has many more features that work very similarly and other Azure Cognitive Services work with the same concepts.

| Service Name                                                                                           | Service Description                                                                                                                     | Feature Name                                                                                           | Feature Description                                                                                                                     |
| :----------------------------------------------------------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------- | :----------------------------------------------------------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------- |
| [Azure Cognitive Service for Language](https://docs.microsoft.com/en-us/azure/cognitive-services/language-service/overview) | Provide natural language processing over raw text for understanding and analyzing text. | [Sentiment Analysis and Opinion Mining](https://docs.microsoft.com/en-us/azure/cognitive-services/language-service/overview) | Extract the sentiment of text and associate positive and negative sentiment with specific aspects of the text. |

## Create a Cognitive Service

We are going to start off by creating an Azure Cognitive Service using the Azure CLI.

1. First we will create a new resource group. Enter the following in your terminal:
    ```shell
    az group create -n rg-azdc-cognitive -l westeurope
    ```
1. When creating the Cognitive Service itself you have two choices. You can either create a multi-service resource or a single-service resource. The multi-service resource gives you access to multiple Azure Cognitive Services with a single key and endpoint. The Single-service resource will allow you to access a single Azure Cognitive Service with a unique key and endpoint.
Since we will only use the Cognitive Service for Language in this challenge we will create a single-service resource.
    ```shell
    az cognitiveservices account create --name cog-textanalytics-westeurope-001 --resource-group rg-azdc-cognitive --kind TextAnalytics --sku S --location westeurope --yes
    ```

## Analyze Sentiment and Opinions

We are going to create a simple node application to test the service. 

1. Create a new folder on your local machine, where the project will reside later on. Name it `TextAnalytics`. 
    ```shell
    mkdir TextAnalytics
    ```
    Navigate to the newly created Folder.
    ```shell
    cd TextAnalytics
    ```

1. Create a node application with a package.json file. Accept all defaults.
    ```shell
    npm init
    ```
    And install the client library
    ```shell
    npm install @azure/ai-text-analytics@5.1.0
    ```
    Now open the application in VS Code.
    ```shell
    code .
    ```

1. Create a new `sentiment.js` file and add the following code:
    ```javascript
    "use strict";

    const { TextAnalyticsClient, AzureKeyCredential } = require("@azure/ai-text-analytics");
    const key = '<API-KEY>';
    const endpoint = 'https://westeurope.api.cognitive.microsoft.com/';
    // Authenticate the client with your key and endpoint
    const textAnalyticsClient = new TextAnalyticsClient(endpoint,  new AzureKeyCredential(key));

    // Example method for detecting sentiment in text
    async function sentimentAnalysis(client){

        const sentimentInput = [
            "I had the best day of my life. I wish you were there with me."
        ];
        const sentimentResult = await client.analyzeSentiment(sentimentInput);

        sentimentResult.forEach(document => {
            console.log(`ID: ${document.id}`);
            console.log(`\tDocument Sentiment: ${document.sentiment}`);
            console.log(`\tDocument Scores:`);
            console.log(`\t\tPositive: ${document.confidenceScores.positive.toFixed(2)} \tNegative: ${document.confidenceScores.negative.toFixed(2)} \tNeutral: ${document.confidenceScores.neutral.toFixed(2)}`);
            console.log(`\tSentences Sentiment(${document.sentences.length}):`);
            document.sentences.forEach(sentence => {
                console.log(`\t\tSentence sentiment: ${sentence.sentiment}`)
                console.log(`\t\tSentences Scores:`);
                console.log(`\t\tPositive: ${sentence.confidenceScores.positive.toFixed(2)} \tNegative: ${sentence.confidenceScores.negative.toFixed(2)} \tNeutral: ${sentence.confidenceScores.neutral.toFixed(2)}`);
            });
        });
    }
    sentimentAnalysis(textAnalyticsClient)

    // Example method for detecting opinions in text 
    async function sentimentAnalysisWithOpinionMining(client){

    const sentimentInput = [
        {
        text: "The food and service were unacceptable, but the concierge were nice",
        id: "0",
        language: "en"
        }
    ];
    const results = await client.analyzeSentiment(sentimentInput, { includeOpinionMining: true });

    for (let i = 0; i < results.length; i++) {
        const result = results[i];
        console.log(`- Document ${result.id}`);
        if (!result.error) {
        console.log(`\tDocument text: ${sentimentInput[i].text}`);
        console.log(`\tOverall Sentiment: ${result.sentiment}`);
        console.log("\tSentiment confidence scores:", result.confidenceScores);
        console.log("\tSentences");
        for (const { sentiment, confidenceScores, opinions } of result.sentences) {
            console.log(`\t- Sentence sentiment: ${sentiment}`);
            console.log("\t  Confidence scores:", confidenceScores);
            console.log("\t  Mined opinions");
            for (const { target, assessments } of opinions) {
            console.log(`\t\t- Target text: ${target.text}`);
            console.log(`\t\t  Target sentiment: ${target.sentiment}`);
            console.log("\t\t  Target confidence scores:", target.confidenceScores);
            console.log("\t\t  Target assessments");
            for (const { text, sentiment } of assessments) {
                console.log(`\t\t\t- Text: ${text}`);
                console.log(`\t\t\t  Sentiment: ${sentiment}`);
            }
            }
        }
        } else {
        console.error(`\tError: ${result.error}`);
        }
    }
    }
    sentimentAnalysisWithOpinionMining(textAnalyticsClient)
    ```

1. Before you can run the code you need to add your Azure Text Analytics Service key in it. You can obtain this information by running the following command:
    ```shell
    az cognitiveservices account keys list --name cog-textanalytics-westeurope-001 --resource-group rg-azdc-cognitive
    ```

1. Save the changes and run the code.
    ```shell
    node sentiment.js
    ```

The result is measured as positive if it's scored closer to 1.0 and negative if it's scored closer to 0.0. The sentiment scores are also associated with different targets within the given text.
This result is returned in JSON, as you can see here:

```json
ID: 0
        Document Sentiment: positive
        Document Scores:
                Positive: 1.00  Negative: 0.00  Neutral: 0.00
        Sentences Sentiment(2):
                Sentence sentiment: positive
                Sentences Scores:
                Positive: 1.00  Negative: 0.00  Neutral: 0.00
                Sentence sentiment: neutral
                Sentences Scores:
                Positive: 0.21  Negative: 0.02  Neutral: 0.77

- Document 0
        Document text: The food and service were unacceptable, but the concierge were nice
        Overall Sentiment: positive
        Sentiment confidence scores: { positive: 0.84, neutral: 0, negative: 0.16 }
        Sentences
        - Sentence sentiment: positive
          Confidence scores: { positive: 0.84, neutral: 0, negative: 0.16 }
          Mined opinions
                - Target text: food
                  Target sentiment: negative
                  Target confidence scores: { positive: 0.01, negative: 0.99 }
                  Target assessments
                        - Text: unacceptable
                          Sentiment: negative
                - Target text: service
                  Target sentiment: negative
                  Target confidence scores: { positive: 0.01, negative: 0.99 }
                  Target assessments
                        - Text: unacceptable
                          Sentiment: negative
                - Target text: concierge
                  Target sentiment: positive
                  Target confidence scores: { positive: 1, negative: 0 }
                  Target assessments
                        - Text: nice
                          Sentiment: positive
```