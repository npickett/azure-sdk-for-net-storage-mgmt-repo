# azure-sdk-for-net-storage-mgmt-repo

## Setup

To reproduce authentication issue with the Storage Management SDK, follow these steps:

1. Fill out App Settings in WebRole\Web.Config.
	a. Insert your Azure Management Footprint in CertMgmtFootprint
	b. Insert your Account Name in AccountName1
	c. Insert your Subscription ID in SubID1

```
    <add key="CertMgmtFootprint" value="InsertCertMgmtFootprint" />
    <add key="AccountName1" value="InsertAccountName1"/>
    <add key="SubID1" value="InsertSubID1"/>
```

2. Set "Local Development Server" to "Use IIS Web Service"
	a. Click on the cloud project, "Azure.Web" and select properties
	b. Select "Use IIS Web Service" under "Local Development Server"

3. Browse "WebRole" project in Visual Studio and right click on "TestCredsLookup.aspx" and select "Set As Start Page"

4. Press F5 in Visual Studio.	


It will reproduce the following error:

```
<Error xmlns="http://schemas.microsoft.com/windowsazure" xmlns:i="http://www.w3.org/2001/XMLSchema-instance"><Code>ForbiddenError</Code><Message>The server failed to authenticate the request. Verify that the certificate is valid and is associated with this subscription.</Message></Error>
```


## Workaround

Set "Local Development Server" to "Use IIS Express"