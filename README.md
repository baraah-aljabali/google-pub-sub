# Google Pub/Sub .NET Core Enhanced Solution

A comprehensive, production-ready Google Cloud Pub/Sub implementation for .NET Core applications with modern architecture patterns, dependency injection, and RESTful API endpoints.

## 🚀 Features

### Enhanced Architecture
- **Modular Design**: Separated Publisher and Subscriber into independent libraries
- **Dependency Injection**: Clean service registration and configuration management
- **RESTful API**: Swagger-enabled HTTP endpoints for easy integration
- **Modern .NET 8**: Latest framework with improved performance and features
- **CORS Support**: Cross-origin resource sharing enabled for web applications

### Publisher Service
- **Batch Publishing**: Support for publishing multiple messages simultaneously
- **Error Handling**: Robust exception handling with detailed logging
- **Async Operations**: Fully asynchronous implementation for better performance
- **Configuration Management**: Flexible options pattern for easy customization

### Subscriber Service
- **Dynamic Subscription**: Automatic subscription creation with conflict handling
- **Message Acknowledgment**: Configurable ACK/NACK behavior
- **Real-time Processing**: Live message consumption with configurable timeouts
- **Thread Safety**: Thread-safe message collection and processing

## 📁 Project Structure

```
Google.PubSub.NetCore.Soution/
├── Google.PubSub.NetCore.Demo/          # Web API Demo Application
│   ├── Controllers/                     # REST API Controllers
│   │   ├── PublisherController.cs       # Message publishing endpoints
│   │   └── SubscriberController.cs      # Message subscription endpoints
│   ├── Program.cs                       # Application startup and configuration
│   └── appsettings.json                 # Configuration settings
├── Google.PubSub.Producer/              # Publisher Library
│   ├── PublisherService.cs              # Core publishing logic
│   ├── GooglePublisherRegistration.cs   # DI registration
│   └── GooglePublisherOptions.cs        # Configuration options
└── Google.PubSub.Subscriber/            # Subscriber Library
    ├── SubscriberService.cs             # Core subscription logic
    ├── GoogleSubscribeRegistration.cs   # DI registration
    └── GoogleSubscriberOptions.cs       # Configuration options
```

## 🛠️ Technology Stack

- **.NET 8.0**: Latest LTS version with performance improvements
- **Google.Cloud.PubSub.V1 3.8.0**: Official Google Cloud Pub/Sub client library
- **ASP.NET Core**: Modern web framework with built-in DI container
- **Swagger/OpenAPI**: Interactive API documentation
- **Microsoft.Extensions.DependencyInjection**: Dependency injection framework

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- Google Cloud Platform account with Pub/Sub enabled
- Google Cloud credentials file (`gcp_pubsubsetting.json`)

### Configuration

#### 🔐 Setting Up Google Cloud Credentials

**Prerequisites:**
- Ensure you have a Google Cloud Project created
- Enable the **Cloud Pub/Sub API** in your project:
  - Go to [Google Cloud Console](https://console.cloud.google.com/)
  - Navigate to **APIs & Services** > **Library**
  - Search for "Cloud Pub/Sub API"
  - Click on it and press **Enable**

1. **Create a service account** in Google Cloud Console:
   - Go to [Google Cloud Console](https://console.cloud.google.com/)
   - Navigate to **IAM & Admin** > **Service Accounts**
   - Click **Create Service Account**
   - Give it a name (e.g., "pub-sub-service")
   - Add a description (optional)
   - Click **Create and Continue**

2. **Assign Pub/Sub permissions**:
   - In the **Grant this service account access to project** step
   - Add the following roles:
     - **Pub/Sub Publisher** (for publishing messages)
     - **Pub/Sub Subscriber** (for subscribing to messages)
     - **Pub/Sub Editor** (for managing topics and subscriptions)
   - Click **Continue**

3. **Create and download the key**:
   - Click **Done** to create the service account
   - Find your service account in the list and click on it
   - Go to the **Keys** tab
   - Click **Add Key** > **Create new key**
   - Choose **JSON** format
   - Click **Create** - this will download the credentials file

2. **Set up credentials securely**:
   ```bash
   # Copy the template and rename it
   cp Google.PubSub.NetCore.Soution/gcp_pubsubsetting.template.json Google.PubSub.NetCore.Soution/gcp_pubsubsetting.json
   
   # Replace placeholder values with your actual credentials
   # ⚠️ NEVER commit the actual credentials file to version control
   ```

3. **Update the credentials path** in `Program.cs`:
   ```csharp
   private static string _setting_path = @"path/to/your/gcp_pubsubsetting.json";
   ```

4. **Configure your Pub/Sub settings** in `appsettings.json`:
   ```json
   {
     "GooglePublisher": {
       "ProjectId": "your-project-id",
       "TopiId": "your-topic-id"
     },
     "GoogleSubscriber": {
       "ProjectId": "your-project-id",
       "SubscriptionId": "your-subscription-id",
       "TopiId": "your-topic-id"
     }
   }
   ```

#### 🔒 Security Best Practices

- **Environment Variables**: For production, use environment variables instead of hardcoded paths:
  ```csharp
  var credentialsPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
  System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
  ```

- **Azure Key Vault**: Store credentials in Azure Key Vault for enhanced security
- **Google Cloud IAM**: Use least-privilege principle for service account permissions
- **Rotation**: Regularly rotate service account keys

### Running the Application

1. **Build the solution**:
```bash
dotnet build Google.PubSub.NetCore.Soution/Google.PubSub.NetCore.Soution.sln
```

2. **Run the demo application**:
```bash
cd Google.PubSub.NetCore.Soution/Google.PubSub.NetCore.Demo
dotnet run
```

3. **Access the API documentation**:
   - Swagger UI: `https://localhost:5001/swagger`
   - API Base URL: `https://localhost:5001/api`

## 📡 API Endpoints

### Publisher API
- **POST** `/api/publisher/register`
  - Publishes messages to Google Cloud Pub/Sub
  - Request body: `PublisherRequest` with message texts array
  - Returns: Number of successfully published messages

### Subscriber API
- **POST** `/api/subscriber/subscribe`
  - Pulls and processes messages from Google Cloud Pub/Sub
  - Request body: `SubscriberRequest` with subscription name and acknowledgment settings
  - Returns: List of received messages

## 🔒 Security Considerations

### Credential Management
- **Never commit credentials**: The `gcp_pubsubsetting.json` file is excluded from version control
- **Use templates**: Copy `gcp_pubsubsetting.template.json` and fill in your values
- **Environment variables**: Store credentials as environment variables in production
- **Secret management**: Use Azure Key Vault, AWS Secrets Manager, or similar services

### Access Control
- **Least privilege**: Grant minimum required permissions to service accounts
- **IAM roles**: Use specific Pub/Sub roles instead of broad permissions
- **Key rotation**: Regularly rotate service account keys
- **Audit logging**: Enable Cloud Audit Logs to monitor access

### Network Security
- **HTTPS only**: Ensure all API communications use HTTPS
- **VPC**: Consider using Google Cloud VPC for network isolation
- **Firewall rules**: Restrict access to necessary IP ranges only

### Application Security
- **Input validation**: Validate all incoming message data
- **Error handling**: Avoid exposing sensitive information in error messages
- **Logging**: Be careful not to log sensitive data
- **Dependencies**: Keep all NuGet packages updated for security patches

## 📈 Performance Features

- **Async/Await**: Non-blocking operations throughout
- **Batch Processing**: Efficient handling of multiple messages
- **Connection Pooling**: Reusable client connections
- **Thread Safety**: Concurrent message processing support

## 🤝 Contributing

This enhanced solution builds upon the original Google Pub/Sub sample, adding:
- Modern .NET 8 architecture
- Clean separation of concerns
- Production-ready error handling
- Comprehensive API documentation
- Easy integration patterns

## 🔐 Security Improvements Made

This enhanced solution includes several security improvements over the original:

- **Credential Protection**: Sensitive GCP credentials are now properly excluded from version control
- **Template System**: A template file provides structure without exposing actual credentials
- **Comprehensive .gitignore**: Prevents accidental commits of sensitive files
- **Security Documentation**: Detailed security best practices and considerations
- **Environment Variable Support**: Production-ready credential management.

---
## 📄 License

This project is based on Google Cloud Pub/Sub samples and enhanced for production use.



**Note**: Remember to update the Google Cloud credentials path and project configuration before running the application. Always use the template file and never commit actual credentials to version control. 
