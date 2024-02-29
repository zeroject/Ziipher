# Ziipher - A Twitter Clone

Welcome to Ziipher a twitter clone

## Services and Functionality

1. **Auth:** Handles authorization and authentication. - Kasper
2. **User:** Manages CRUD operations for user profiles. - Casper
3. **Post:** Manages CRUD operations for posts in the timeline. - Issa
4. **Comment:** Manages CRUD operations for comments on posts. - Casper
5. **DMs:** Provides basic messaging functions for direct messages. - Anders

### Auth
![Auth Illustration](https://github.com/zeroject/Ziipher/assets/91524039/ae2cb0bc-0a60-4262-a084-384d2fe049aa)
- Responsible for authorization and authentication.

### User
![image](https://github.com/zeroject/Ziipher/assets/47432671/e0f6fab9-758c-49c7-b3fb-bd00536bc165)
- Handles CRUD operations for user profiles.

### Post
- Handles CRUD operations for posts in the timeline.

### Comment
![image](https://github.com/zeroject/Ziipher/assets/47432671/c378c2ea-1a62-47c2-8ce1-efe063cf362a)
- Handles CRUD operations for comments on posts.

### DMs
- Provides basic messaging functions for direct messages.

## Launching with Docker

Each service can be launched individually using Docker. Ensure you have Docker installed on your system before proceeding.

### Instructions
1. Navigate to the root directory of each service.
2. Build the Docker image using the provided Dockerfile.
3. Run the Docker container using the appropriate command.

### Dependencies

- Each service may depend on others for certain functionalities. Make sure all required services are running for the full functionality of Ziipher.

## Installation

To install Ziipher on your system, follow these steps:

1. Clone the Ziipher repository to your local machine.
2. Install Docker if not already installed.
3. Follow the Docker instructions provided above to launch each service.
4. Once all services are running, navigate to the provided UI console to interact with Ziipher.
