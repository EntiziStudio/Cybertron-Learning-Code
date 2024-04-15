# Primus

This project is built with Next.js and requires a specific version of Node.js managed by NVM (Node Version Manager). This guide will help you set up the project and install the correct version of Node.js using the `.nvmrc` file.

## Prerequisites

Before you begin, make sure you have the following prerequisites installed on your system:

- NVM (Node Version Manager): You can install it by following the instructions below.

## Installing NVM (Windows)

To install NVM on Windows, follow these steps:

1. Download the NVM Windows installer from the official GitHub repository: [https://github.com/coreybutler/nvm-windows/releases](https://github.com/coreybutler/nvm-windows/releases)

2. Scroll down to the **Assets** section and download the latest `nvm-setup.zip` file.

3. Once the download is complete, extract the contents of the ZIP file to a directory of your choice. For example, `C:\nvm`.

4. Open a Command Prompt or PowerShell window with administrative privileges.

5. Navigate to the directory where you extracted the `nvm-setup.zip` file.

6. Run the `nvm-setup.exe` file to start the NVM installation wizard.

7. Follow the instructions in the installation wizard to complete the installation process. Make sure to select the desired installation options, such as the installation directory.

8. After the installation is complete, close the Command Prompt or PowerShell window and open a new one to ensure that the NVM command is recognized.

9. Test the NVM installation by running the following command:

   ```bash
   nvm --version
   ```

   If NVM is installed correctly, you should see the version number displayed in the console.

10. NVM is now successfully installed on your Windows system.

## Getting Started

To get started with the project, follow these steps:

1. Clone the project repository to your local machine.

   ```bash
   git clone https://gitlab.com/cybertron_planet/primus.git
   ```

2. Change into the project directory.

   ```bash
   cd primus
   ```

3. Install the required version of Node.js using NVM.

   ```bash
   nvm install 18
   ```

   NVM will read the `.nvmrc` file in the project directory and install the specified version of Node.js automatically.

4. Set the installed Node.js version as the default for the project.

   ```bash
   nvm use 18
   ```

   This command will set the Node.js version specified in `.nvmrc` as the default for this project. Whenever you navigate to this project directory, NVM will automatically switch to the specified version.

5. Install project dependencies.

   ```bash
   npm install 18
   ```

   This command will install all the required dependencies for the project.

6. Start the development server.

   ```bash
   npm run dev
   ```

   This command will start the development server, and you can now access your project in your browser at `http://localhost:2023`.

## Copyright

The Allspark project is confidential and proprietary to HUTECH. Unauthorized copying, distribution, or disclosure of this project or its contents is strictly prohibited.

© 2023 HUTECH. All rights reserved.