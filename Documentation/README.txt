---------READ ME----------

In this readme file are contained all the neccessary steps for our application to work:

1. Starting the REST Service
	
	1.1 Go to the /ilift-webservices directory, where you can find an eclipse projcet, and execute the following command:

		mvn clean compile assembly:single

	1.2 Afterwards you can start the services with the following command:

		java -jar target/ilift-webservices-0.0.1-SNAPSHOT-jar-with-dependencies.jar


2. Web Client Configuration

	Before trying to access the data from the web after you have started successfully the web services
	make sure you perform this step:

	2.1 Update the IP address in /ilift-webclient/config.js accordingly with the IP address where your web service is listening. 

	2.2 Open ilift.html file.

3. .NET Gadgeeter 

	3.1 You can find the .NET Gadgeteer solution inside the /ilift-gadgeteer folder. 

	3.2 The only thing that needs to be configured are the network parameters that you can find 
	in the NetworkClient class in the Network package and then you can deploy the application

	3.3 Finally you can deploy it in the Gadgeteer hardware.
