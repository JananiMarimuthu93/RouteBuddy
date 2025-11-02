RouteBuddy - Epic Features & User Stories Documentation
Project: RouteBuddy Bus Booking System
Version: 1.0
Date: November 2025
Author:Intern
Document Type: Epic Features & User Stories for Azure DevOps

Table of Contents
 1.Epic-1: Customer Account Management

User Story ID: US-AM-001 – Manage Customer Profile
Title:
Customer Profile Management – View and Update Customer Details

Description:

As a registered customer,
I want to view and update my personal profile information,
so that I can maintain accurate details like name, date of birth, gender, and profile picture in my account.

This story enables customers to view and update their personal information stored in the Users and Customers tables.
The feature supports the My Profile page, which retrieves user details, allows editing, validates input, uploads a profile picture, and logs updates.


Preconditions:

The user is logged in successfully and has a valid session.
A record exists in the Customers table with a valid UserId reference.
Email and Phone fields are verified and cannot be edited.
The “My Profile” page is accessible from the user dashboard menu.
The backend APIs /api/customer/{customerId} and /api/customer/update are active and connected to the frontend.

Acceptance Criteria
Scenario 1: Navigate to “My Profile” Page
Given I am logged into the RouteBuddy web application

The dashboard menu is visible.

The “My Profile” option is available under the user dropdown (top-right corner).

When I click on the “My Profile” option

The system triggers a GET /api/customer/{customerId} request to fetch the user profile data.

A loading spinner appears with text: “Fetching your profile…”.

Then

The page displays:

First Name, Middle Name, Last Name

Date of Birth (DD-MM-YYYY format)

Gender (Dropdown: Male, Female, Other)

Email (disabled input field)

Phone (disabled input field)

Profile Picture (thumbnail or default avatar)

Computed values: Full Name and Age (read-only fields)

The “Edit Profile” and “Upload Picture” buttons are visible.
