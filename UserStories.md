Project: RouteBuddy Bus Booking System
Version: 1.0Date: November 2025Author: InternDocument Type: Epic, Features & User Stories for Azure DevOps

Epic 1: Customer Profile Management
Epic Description:
This Epic manages all aspects of the Customer Profile Module, including viewing, editing, and maintaining personal details and profile pictures.It ensures user data integrity, validation compliance, and logging consistency using the Users and Customers tables in the RouteBuddy database.

Feature 1: Customer Profile Management
Feature Description:
This feature allows a registered customer to:

View their personal profile details

Update editable fields such as first name, last name, date of birth, and gender

Upload or change a profile picture

Maintain audit logs and EF Core validation compliance

Authentication, email/phone verification, and account activation/deactivation are managed separately by other modules.

User Story ID: US-BS-001 – Manage Customer Profile
Title:
Customer Profile Management – View and Update Customer Details

Description:
As a registered customer,I want to view and update my personal profile information,so that I can maintain accurate details like name, date of birth, gender, and profile picture in my account.

This story enables customers to view and update their personal information stored in the Users and Customers tables.The feature supports the My Profile page, which retrieves user details, allows editing, validates input, uploads a profile picture, and logs updates.

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

The “My Profile” option is available at top right.

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

Scenario 2: Edit Customer Profile
Given I am on the “My Profile” page

I can see the Edit Profile button in the top-right section.
When I click the Edit Profile button

All editable fields (First Name, Middle Name, Last Name, Date of Birth, Gender) become enabled.

The Save Changes and Cancel buttons appear below the form.

Validation indicators (red asterisks for required fields) are shown beside First Name, Last Name, and Date of Birth.

Then I can modify:

First Name: accepts alphabetic characters only, max 50 chars.

Middle Name: optional, alphabetic only, max 50 chars.

Last Name: required, alphabetic, max 50 chars.

Date of Birth: opens a date picker (must be a past date).

Gender: dropdown options (Male, Female, Other).

When I click Save Changes

The system validates each field using EF Core annotation rules.

If invalid:

Inline validation messages appear, e.g.,

“First Name is required.”

“Invalid Date of Birth.”

If valid:

The frontend calls PUT /api/customer/update with the updated data.

A loading bar appears with text: “Saving your details…”.

Upon success:

API response includes updated customer details.

The form auto-disables and switches back to view mode.

✅ “Profile updated successfully.”

And the system logs this event:

MagicStrings.Logging.CustomerProfileUpdate with timestamp, UserId, and updated fields.

The UpdatedBy and UpdatedOn fields in the database are refreshed.

Scenario 3: Upload Customer Profile Picture
Given I am on the “My Profile” page

My current profile picture or placeholder avatar is displayed.
When I hover over the profile image

A tooltip appears: “Change Profile Picture”.

A camera icon (📷) overlay is shown.

When I click the icon

A file picker opens allowing selection of .jpg, .jpeg, or .png files (max size: 2 MB).

Once a file is selected:

A preview of the selected image is shown.

The Upload and Cancel buttons appear below the preview.

When I click Upload

The frontend validates the file type and size.

⚠️ “Invalid file type or size. Please upload a JPG/PNG image under 2 MB.”

If valid:

The image is sent to PUT /api/customer/profile-picture.

Backend stores the image in the Customers.ProfilePicture column (VARBINARY(MAX)).

The image on the UI updates instantly to the new picture.

✅ “Profile picture updated successfully.”

And the system logs:

MagicStrings.Logging.CustomerProfilePictureUpdated event with timestamp and UserId.
Scenario 4: Validation and Data Integrity
Given I try to submit a form with invalid or missing valuesWhen I click Save Changes

The system performs validation based on EF Core annotations:

[Required], [MaxLength(50)], [Comment], [ForeignKey].
If invalid:

Red border highlights the field.

Inline validation messages appear below each field.

No API call is triggered until validation passes.

Then

Only valid data is persisted.

The computed properties FullName and Age remain [NotMapped].

Data integrity between User and Customer remains intact.

Database Actions
Tables Involved:

Users Table – (Email, Phone, Role, IsEmailVerified, IsActive)

Read-only in this module.
Customers Table – (FirstName, LastName, DateOfBirth, Gender, ProfilePicture, UserId, UpdatedBy, UpdatedOn)

Updated when user saves changes.
On successful update:

Record is updated with new data.

UpdatedBy = Current UserId.

UpdatedOn = Current UTC Timestamp.

Audit trail logs entry in MagicStrings.Logging.

API Endpoints
Operation	Endpoint	Method	Description
Fetch Profile	/api/customer/{customerId}	GET	Retrieves combined User + Customer info
Update Profile	/api/customer/update	PUT	Updates editable fields
Upload Picture	/api/customer/profile-picture	PUT	Uploads or replaces profile image
Postconditions
Customer details are successfully updated in the database.

Profile picture (if uploaded) appears immediately on refresh.

Logging and audit records are created.

Computed fields (FullName, Age) remain correct.

Error Handling
Error Case	Display Message	System Action
Missing required field	“This field is required.”	Prevent submission
Invalid DOB	“Date of birth cannot be in the future.”	Prevent submission
Invalid gender	“Select a valid gender.”	Prevent submission
API failure	“Unable to update profile. Please try again later.”	Rollback UI changes
Image too large	“Image exceeds 2 MB limit.”	Reject upload
Invalid format	“Only JPG, JPEG, PNG allowed.”	Reject upload
Definition of Done (DoD)
All acceptance criteria are satisfied.

API and UI integration tested successfully.

EF Core data annotation validation verified.

Logging implemented using MagicStrings.

Unit & integration test coverage ≥ 80%.

Verified and deployed to staging environment.

QA and Product Owner sign-off completed.
