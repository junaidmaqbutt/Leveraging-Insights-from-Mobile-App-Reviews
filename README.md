# Leveraging-Insights-from-Mobile-App-Reviews

This repository contains files used to parse the data provided by the reasearch study ([Link](https://junaidmaqsood.com/LI-MAR-S-RPAM/)).

There exist three different classes provided in this repository. Lets look into them individually.

Class : **AppDetails**

This class provide a skeleton template for the information provided via the Apple RSS feed ([LINK](https://rss.itunes.apple.com/en-us)) for an app detail. There exist two major functions in this class. The ToString() method is overwritten to provide a single line String representation of the complete object. This the the same pattern used to write all data in the "All AppDetails" dataset provided via the research study. In order to parse a string back to its original object form we use the method "Parse" . This method takes one parametor (String : A single line string representing one object) and returns the Object (AppDetails : An object representation of the string value)

Class : **ReviewDetails**

Similar to the AppDetails Class. This class can be used to output the Review information reviewed via the App RSS Feed. The class comes with the same two functions (ToString and Parse) both work exactly the same way and are used to either output the object to string for save purposes or to retrieve an object from string value. 

Class : **SummaryData**

This is the Class used to structure Clusters. We provide various properties used in the study. Some will require the use of external libraries like OTS and Sentiment. The class follow a traditional way of development with the ToString function converting it into proper string value for storage and the Parse function conveting the string back to this object. This also comes with run time properties to find the appropriate sentiment of the cluster through the given reviews.