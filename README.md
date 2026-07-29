# NetBird Machine Status Check

## Section 1: Project Overview

The **NetBird Machine Status Check** application was developed to simplify the process of verifying the VPN connectivity status of company computers using **NetBird VPN**. The tool provides IT administrators with a fast, lightweight method for determining whether a workstation is currently connected to the organization's secure VPN network without manually searching through the NetBird administration portal.

Designed for use by the IT department supporting both **Emerald Engineering** and **Copperline Electric**, the application queries the appropriate NetBird environment and returns the connection status of the specified computer in seconds. By automating this lookup process, the tool reduces troubleshooting time and enables IT staff to quickly determine whether connectivity issues are related to VPN availability.

---

# Section 2: Business Problem

## Challenge

Emerald Engineering and Copperline Electric each maintain separate NetBird VPN environments containing dozens of managed workstations. During remote support sessions, IT technicians frequently need to determine whether a user's computer is currently connected to the corporate VPN.

Prior to developing this application, technicians were required to:

- Log into the NetBird administration portal.
- Navigate to the appropriate company environment.
- Search for the computer manually.
- Verify its current connection status.

This repetitive process consumed valuable time, especially during support calls where quick diagnosis was critical.

Additionally, technicians had to know which NetBird environment the computer belonged to before performing the search, increasing the likelihood of searching the wrong environment.

## Impact

Without automation, IT support staff experienced:

- Longer troubleshooting times
- Increased administrative effort
- Slower response during remote support
- Potential searches in the wrong NetBird environment
- Reduced efficiency when supporting multiple companies

The organization required a simple utility that could quickly retrieve VPN connection status while minimizing manual interaction with the NetBird administrative interface.

---

# Section 3: Technical Solution

To address this challenge, I developed a console application in **C#** using **JetBrains Rider** that interfaces with the NetBird API to retrieve machine connectivity information.

Upon launching the application, the administrator is prompted to enter the target computer name. After the computer name is entered, the application prompts the administrator to specify the company environment by selecting:

- **1** — Emerald Engineering
- **2** — Copperline Electric

The application then searches the selected NetBird environment for the specified machine.

If the computer is found, the application displays:

- NetBird Environment
- NetBird Machine Name
- Current VPN Status

The connection status is color coded for easy identification:

- **Connected** — displayed in green
- **Disconnected** — displayed in red

After displaying the results, the application prompts the administrator to press any key to exit.

If the specified computer cannot be located, the application returns a descriptive error message indicating that the machine was not found in the selected NetBird environment. This validation helps administrators quickly identify incorrect computer names or company selections.

The application provides a significantly faster alternative to manually searching the NetBird administrative portal, allowing IT staff to verify VPN connectivity within seconds.

---

# Section 4: Technologies Used

## Programming Languages

- C#

## Framework

- .NET

## Development Environment

- JetBrains Rider

## APIs

- NetBird Management API

## Technologies

- REST API
- JSON
- Console Application
- Environment Variables
- HTTP Client

## Infrastructure

- NetBird VPN
- Emerald Engineering Environment
- Copperline Electric Environment

---

# Section 5: Screenshots

The following screenshots illustrate the application's workflow and demonstrate its functionality.

---

## 1. Application Startup

The application launches and prompts the administrator to enter the target computer name.

<img width="860" height="460" alt="image" src="https://github.com/user-attachments/assets/49385e17-0805-4653-9518-4c3eb48bb317" />

---

## 2. Computer Name Entry

Administrator entering the workstation name to retrieve its current NetBird VPN connection status.

<img width="1768" height="698" alt="image" src="https://github.com/user-attachments/assets/04c2c261-59d3-46a6-92c9-b3aff820aca5" />

---

## 3. Environment Selection

**Caption**

Prompt requesting the company environment, allowing the administrator to select either the Emerald Engineering or Copperline Electric NetBird environment.

*(Insert Screenshot Here)*

---

## 4. Successful Connected Status

**Caption**

Successful lookup showing the selected NetBird environment, machine name, and a **Connected** status displayed in green.

*(Insert Screenshot Here)*

---

## 5. Successful Disconnected Status

**Caption**

Successful lookup showing the machine is currently **Disconnected**, highlighted in red for immediate visibility.

*(Insert Screenshot Here)*

---

## 6. Invalid Computer Name

**Caption**

Error handling when a computer name cannot be located within the selected NetBird environment.

*(Insert Screenshot Here)*

---

## 7. Incorrect Environment Selection

**Caption**

Application response when a valid computer name is searched in the wrong NetBird environment.

*(Insert Screenshot Here)*

---

## 8. Application Completion

**Caption**

Final screen displaying the lookup results and prompting the administrator to press any key to exit the application.

*(Insert Screenshot Here)*

---

## 9. Source Code Overview (Optional)

**Caption**

Overview of the C# project in JetBrains Rider, illustrating the application's structure and organization.

*(Insert Screenshot Here)*

---

## 10. API Configuration (Optional)

**Caption**

Example of the application's configuration (with sensitive values redacted), demonstrating support for multiple NetBird environments.

*(Insert Screenshot Here)*

---

# Section 6: Lessons Learned

Developing this application provided valuable experience integrating enterprise networking tools with custom software solutions to improve IT operational efficiency.

## Key Lessons Learned

- Working with RESTful APIs in C#
- Parsing JSON responses
- Managing multiple API environments
- Implementing console-based user interfaces
- Validating user input
- Handling API and network errors gracefully
- Designing applications for real-world IT support workflows
- Displaying color-coded console output for improved usability
- Managing application configuration using environment variables
- Creating tools that reduce repetitive administrative tasks

The project reinforced the value of automation in IT operations by eliminating repetitive manual searches and enabling faster troubleshooting during remote support sessions.

---

# Section 7: Future Improvements

Several enhancements could further improve the application's usability and scalability.

## Planned Improvements

- Develop a graphical Windows interface (WPF or WinForms)
- Automatically detect the correct company environment
- Support searching by username or serial number
- Display additional machine information (IP address, operating system, last seen timestamp)
- Add batch lookup functionality for multiple computers
- Export lookup results to CSV or Excel
- Add application logging and audit history
- Integrate directly with the company help desk ticketing system
- Add PowerShell support for scripting and automation
- Package the application for deployment through Microsoft Intune

These enhancements would further streamline remote support operations and provide IT administrators with additional diagnostic information while reducing manual effort.

---

# Skills Demonstrated

- C#
- .NET Development
- REST API Integration
- JSON Parsing
- HTTP Client
- Console Application Development
- NetBird VPN Administration
- Network Connectivity Monitoring
- Input Validation
- Error Handling
- Environment Configuration
- IT Process Automation
- Systems Administration
- Enterprise IT Support
- Technical Troubleshooting
