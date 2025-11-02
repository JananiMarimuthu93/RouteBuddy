🚌 RouteBuddy Bus Booking System - Complete Epics & Features
Version: 2.0
Date: November 2025
Status: Production Ready
📋 Table of Contents
Epic 1: Customer Profile Management
Epic 2: My Trips Management
Epic 3: Mock Payment Processing System
Epic 4: Booking Cancellation & Refund Management
Epic 5: Customer Review & Rating System

🎯 EPIC 1: CUSTOMER PROFILE MANAGEMENT
Epic Description
Enable registered customers to view, edit, and manage their personal profile information including name, date of birth, gender, and profile pictures. Ensure data integrity, validation compliance, and comprehensive audit logging.
Feature 1: Customer Profile Management
US-CP-001: Manage Customer Profile
Priority: High | Story Points: 13 | Sprint: 1
User Story
As a registered customer
I want to view and update my personal profile information
So that I can maintain accurate details like name, date of birth, gender, and profile picture in my account
Preconditions
User is logged in with valid session and CustomerId
Record exists in Customers table linked to Users table
Email and Phone are verified and read-only
"My Profile" page accessible from dashboard menu
Backend APIs active: /api/customer/{customerId} and /api/customer/update

Acceptance Criteria
Scenario 1: Navigate to "My Profile" Page
Given I am logged into the RouteBuddy web application
The dashboard menu is visible
The "My Profile" option is available at top right

When I click on the "My Profile" option
The system triggers a GET /api/customer/{customerId} request
A loading spinner appears with text: "Fetching your profile…"

Then
The page displays: First Name, Middle Name, Last Name, Date of Birth, Gender, Email, Phone, Profile Picture
Computed values: Full Name and Age (read-only fields)
The "Edit Profile" and "Upload Picture" buttons are visible

Scenario 2: Edit Customer Profile
Given I am on the "My Profile" page
I can see the Edit Profile button in the top-right section

When I click the Edit Profile button
All editable fields (First Name, Middle Name, Last Name, Date of Birth, Gender) become enabled
The Save Changes and Cancel buttons appear below the form
Validation indicators (red asterisks for required fields) are shown

Then I can modify:
First Name: alphabetic characters only, max 50 chars
Middle Name: optional, alphabetic only, max 50 chars
Last Name: required, alphabetic, max 50 chars
Date of Birth: opens a date picker (must be a past date)
Gender: dropdown options (Male, Female, Other)

When I click Save Changes
The system validates each field using EF Core annotation rules
If invalid: Inline validation messages appear (e.g., "First Name is required")
If valid:


The frontend calls PUT /api/customer/update with updated data
A loading bar appears with text: "Saving your details…"
Upon success: API response includes updated customer details
The form auto-disables and switches back to view mode
✅ "Profile updated successfully" message shown


And the system logs this event:
MagicStrings.Logging.CustomerProfileUpdate with timestamp, UserId, and updated fields
The UpdatedBy and UpdatedOn fields in the database are refreshed

Scenario 3: Upload Customer Profile Picture
Given I am on the "My Profile" page
My current profile picture or placeholder avatar is displayed

When I hover over the profile image
A tooltip appears: "Change Profile Picture"
A camera icon (📷) overlay is shown

When I click the icon
A file picker opens allowing selection of .jpg, .jpeg, or .png files (max size: 2 MB)
Once a file is selected:


A preview of the selected image is shown
The Upload and Cancel buttons appear below the preview


When I click Upload
The frontend validates the file type and size
If invalid: ⚠️ "Invalid file type or size. Please upload a JPG/PNG image under 2 MB"
If valid:


The image is sent to PUT /api/customer/profile-picture
Backend stores the image in the Customers.ProfilePicture column (VARBINARY(MAX))
The image on the UI updates instantly to the new picture
✅ "Profile picture updated successfully"


And the system logs:
MagicStrings.Logging.CustomerProfilePictureUpdated event with timestamp and UserId

Scenario 4: Validation and Data Integrity
Given I try to submit a form with invalid or missing values
When I click Save Changes
The system performs validation based on EF Core annotations:


[Required], [MaxLength(50)], [Comment], [ForeignKey]

If invalid:


Red border highlights the field
Inline validation messages appear below each field

No API call is triggered until validation passes

Then
Only valid data is persisted
The computed properties FullName and Age remain [NotMapped]
Data integrity between User and Customer remains intact

Database Tables Affected
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

Users Table:
├── UserId (PK)
├── Email (Read-only)
├── Phone (Read-only)
├── Role
├── IsEmailVerified
├── IsActive


Customers Table:
├── CustomerId (PK)
├── UserId (FK)
├── FirstName (Editable)
├── MiddleName (Editable)
├── LastName (Editable)
├── DateOfBirth (Editable)
├── Gender (Editable)
├── ProfilePicture (Editable - VARBINARY(MAX))
├── CreatedAt
├── UpdatedAt
├── UpdatedBy (UserId who updated)





Customer Profile Management
API Endpoints
Operation: Fetch Profile

Endpoint: /api/customer/{customerId}

Method: GET

Description: Retrieves combined User + Customer info
Operation: Update Profile

Endpoint: /api/customer/update

Method: PUT

Description: Updates editable fields
Operation: Upload Picture

Endpoint: /api/customer/profile-picture

Method: PUT

Description: Uploads or replaces profile image
Validation Rules
Field: First Name

Rule: Required, alphabetic, max 50

Error Message: "First Name is required and must contain only letters"
Field: Middle Name

Rule: Optional, alphabetic, max 50

Error Message: "Middle Name must contain only letters"
Field: Last Name

Rule: Required, alphabetic, max 50

Error Message: "Last Name is required and must contain only letters"
Field: Date of Birth

Rule: Required, past date

Error Message: "Date of Birth cannot be in the future"
Field: Gender

Rule: Required, one of (M/F/Other)

Error Message: "Please select a valid gender"
Field: Email

Rule: Read-only

Error Message: N/A
Field: Phone

Rule: Read-only

Error Message: N/A
Error Handling
Error Case: Missing required field

Display Message: "This field is required"

System Action: Prevent submission
Error Case: Invalid DOB

Display Message: "Date cannot be in future"

System Action: Prevent submission
Error Case: Invalid gender

Display Message: "Select valid gender"

System Action: Prevent submission
Error Case: API failure

Display Message: "Unable to update. Try later"

System Action: Rollback UI changes
Error Case: Image too large

Display Message: "Image exceeds 2 MB limit"

System Action: Reject upload
Error Case: Invalid format

Display Message: "Only JPG, JPEG, PNG allowed"

System Action: Reject upload
🎯 EPIC 2: MY TRIPS MANAGEMENT
Epic Description
Enable customers to view, manage, and track all their bus trip bookings organized by status (Upcoming, Others including Completed, Cancelled, Pending, Refunded). Provide detailed booking information, payment status, and trip management capabilities.
Feature 1: My Trips - Comprehensive Booking Management
US-MT-001: View All Trip Categories
Priority: High | Story Points: 13 | Sprint: 2
User Story
As a registered customer
I want to view all my bus bookings organized by categories (Upcoming and Others)
So that I can track my travel history, manage current bookings, and access detailed booking information
Preconditions
User is logged in with valid CustomerId
Valid booking records exist in Bookings table linked to CustomerId
System clock synchronized for accurate date filtering
BookingStatus values: Pending, Confirmed, Cancelled
PaymentStatus values: Pending, Success, Failed, Cancelled, Refunded
APIs active: /api/trips/upcoming/{customerId} and /api/trips/others/{customerId}

Acceptance Criteria
Scenario 1: View Upcoming Trips Tab
Given I am logged into the RouteBuddy application
When I navigate to "My Trips" → "Upcoming" tab
Then the system should:
Display only active confirmed bookings where TravelDate >= CurrentDate AND Status = 'Confirmed'
Show bookings sorted by TravelDate ascending (soonest first)
Display each trip card with:


🟢 UPCOMING status badge
PNR, Bus name, Route (Source → Destination)
Travel date & time, Seats booked, Total amount
Action buttons: "View Details", "Cancel Booking", "Download Ticket"


Scenario 2: View Others Tab
Given I am on the "Others" tab of "My Trips"
When the page loads
Then the system should:
Display all non-upcoming bookings (completed, cancelled, pending, refunded)
Show bookings sorted by TravelDate descending (most recent first)
Display each trip card with appropriate status badges:


✅ COMPLETED - Past confirmed trips
❌ CANCELLED - Cancelled bookings with refund info
⏳ PENDING - Payment pending bookings
💰 REFUNDED - Refunded bookings


Scenario 3: Tab-Specific Display and Actions
Given I am viewing either tab
When trip cards are rendered
Then they should display tab-appropriate content:
Upcoming Tab - 🟢 UPCOMING Trips:
Status Message: "Your upcoming trip from Chennai to Bangalore on Nov 10, 2025"
Action Buttons: "View Details", "Cancel Booking", "Download Ticket"
Highlight: Green border, clean active design

Others Tab - Status-Specific Display:
✅ COMPLETED Trips:
Status Message: "Trip completed successfully on Oct 28, 2025"
Action Buttons: "View Details", "Download Invoice", "Rate Trip", "Book Again"

❌ CANCELLED Trips:
Status Message: "Your trip on Oct 30, 2025 was canceled"
Refund Info: "Refund of ₹450 processed on Oct 31, 2025"
Action Buttons: "View Details", "Refund Status", "Book Similar Trip"

⏳ PENDING Trips:
Status Message: "Awaiting payment confirmation"
Action Buttons: "Complete Payment", "Cancel Booking"

💰 REFUNDED Trips:
Status Message: "Refund of ₹450 processed successfully"
Action Buttons: "View Details", "Book Similar Trip"

Scenario 4: Empty State Handling
Given I am on either tab
When no bookings exist for that category
Then the system should display:
Upcoming Tab Empty State:
Message: "🚍 No upcoming trips. Plan your next journey!"
Action Button: "Book Now" → redirects to /bus/search

Others Tab Empty State:
Message: "📅 No travel history yet. Start exploring!"
Action Button: "Book Your First Trip" → redirects to /bus/search

Scenario 5: Detailed Trip View
Given I click "View Details" on any booking
When the detail modal/page opens
Then it should display:
Complete Booking Information: PNR, Booking Date, Travel Date, Passenger details, Bus info, Route details, Seat layout
Payment Information: Payment method, transaction ID, Payment status, Amount breakdown, Refund details (if applicable)
Trip Management Options: Download ticket/invoice, Cancel booking (if eligible), Modify booking (if allowed), Contact support

Scenario 6: Error Handling & Loading States
When API calls are in progress
Then show loading indicators with messages:
"Loading your trips..."
"Fetching booking details..."

When API calls fail
Then display appropriate error messages:
Network Error: "Unable to load trips. Please check your connection"
Server Error: "Something went wrong. Please try again later"
Unauthorized: "Session expired. Please log in again"

Database Tables Involved
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

Bookings - Main booking information
BookedSeats - Seat details and passenger info
Payments - Payment status and transaction details
Cancellations - Cancellation and refund information
BusSchedules - Bus timing and route information
Buses - Bus details and amenities
Routes - Route information with stops
Customers - Customer information



🚌 API Endpoints — Trip Management
Operation: Get Upcoming Trips
Endpoint: /api/trips/upcoming/{customerId}
Method: GET
Description: Fetch active confirmed future bookings
Operation: Get Other Trips
Endpoint: /api/trips/others/{customerId}
Method: GET
Description: Fetch completed, cancelled, pending, and refunded trips
Operation: Get Trip Details
Endpoint: /api/trips/details/{bookingId}
Method: GET
Description: Fetch complete booking information
Operation: Cancel Booking
Endpoint: /api/trips/cancel/{bookingId}
Method: POST
Description: Cancel an upcoming booking
Operation: Download Ticket
Endpoint: /api/trips/ticket/{bookingId}
Method: GET
Description: Generate and download ticket PDF

🎯 EPIC 3: MOCK PAYMENT PROCESSING SYSTEM
Epic Description
Simulate complete payment processing for bus bookings in development and testing environments, including transaction handling, payment confirmations, and refund processing using mock data and simulated payment gateways.
Feature 1: Mock Payment Processing
US-PM-001: Mock Debit Card Payment
Priority: High | Story Points: 13 | Sprint: 3
User Story
As a customer
I want to simulate debit card payments
So that I can test the booking flow end-to-end without real payment processing in development/testing environment
Preconditions
Customer completed bus and seat selection and navigated to Payment Page
Temporary booking record exists with BookingStatus = "Pending"
Mock payment environment is active (Development/Test mode enabled)
Payment Page displays customer details, total fare, bus info, payment methods
Network and database connectivity available

Acceptance Criteria
Scenario 1: Successful Mock Debit Card Payment
Given I am on the payment page with a valid pending booking
The user must arrive at this page only after completing seat selection
The system verifies that a valid booking exists with BookingStatus = "Pending"
The page displays bus name, route, seat number, customer details, total fare, payment method options

Step 1: Card Details Validation
When I enter the test card number "4111111111111111"
The card number input field accepts only numeric digits (0–9)
The field auto-formats the input with spaces after every 4 digits (e.g., 4111 1111 1111 1111)
Validation rules apply:


Card number must be exactly 16 digits
If input is not 16 digits → display error: "Please enter a valid 16-digit card number"

The system auto-detects card type as "Visa" and displays the card brand indicator
Invalid or incomplete input disables the payment submission button

API Call - Validate Card:
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

POST /api/CardPayment/validate-card


Request:
{
  "cardNumber": "4111111111111111"
}


Response (200):
{
  "success": true,
  "data": {
    "isValid": true,
    "cardType": "Visa",
    "message": "Valid card number"
  }
}




And I enter a valid future expiry date and a 3-digit CVV
Expiry date field format: MM/YY (accepts only digits and "/")
Validation rules:


Month must be between 01 and 12
Year must be greater than or equal to the current year
If expiry date is in the past → display error: "Card has expired"

CVV field must accept exactly 3 digits
Invalid CVV entry displays error: "Please enter a valid 3-digit CVV"

Database Query - Validate Booking:
SELECT BookingId, TotalAmount, Status, CustomerId
FROM Bookings 
WHERE BookingId = @BookingId 
  AND Status = 'Pending' 
  AND IsActive = 1  AND DATEDIFF(MINUTE, CreatedOn, GETUTCDATE()) <= 10;

Step 2: Payment Initiation (No OTP Yet)
When I click "Proceed to OTP"
All form input fields are disabled
Button text changes to "Processing..."
A loading spinner or progress indicator is displayed

API Call - Process Payment & Create Pending Record:
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

POST /api/CardPayment/process


Request:
{
  "bookingId": 123,
  "amount": 1500.00,
  "cardNumber": "4111111111111111",
  "cardHolderName": "John Doe",
  "expiryMonth": 12,
  "expiryYear": 2025,
  "cvv": "123"
}


Response (200):
{
  "success": true,
  "data": {
    "paymentId": 456,
    "amount": 1500.00,
    "transactionId": "TXN1234567890",
    "requiresOtp": true,
    "cardType": "Visa",
    "maskedCardNumber": "**** **** **** 1111",
    "message": "OTP sent to registered mobile"
  }
}

Then the system should simulate a 3-second processing delay
Mock payment service enforces 3000 ms delay before returning response
All input fields remain non-interactive during delay
UI displays status message: "Processing payment, please wait..."

Database Operations - Create Payment & OTP Record:
sql
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

-- Validate booking exists and is pending (within 10-minute window)
SELECT BookingId, TotalAmount, Status, CustomerId
FROM Bookings 
WHERE BookingId = @BookingId 
  AND Status = 'Pending' 
  AND IsActive = 1
  AND DATEDIFF(MINUTE, CreatedOn, GETUTCDATE()) <= 10;


-- Create payment record with PENDING status (NOT confirmed yet)
INSERT INTO Payments (BookingId, Amount, PaymentMethod, PaymentStatus, TransactionId, CreatedBy, CreatedOn, IsActive)
VALUES (@BookingId, @Amount, 'DebitCard-Visa', 'Pending', @TransactionId, 'System', GETUTCDATE(), 0);


-- Log OTP generation (mock)
INSERT INTO OtpLogs (PaymentId, OtpSentAt, ExpiresAt, IsActive)
VALUES (@PaymentId, GETUTCDATE(), DATEADD(MINUTE, 5, GETUTCDATE()), 1);


-- Create audit log
INSERT INTO AuditLogs (PaymentId, Action, Details, Timestamp)
VALUES (@PaymentId, 'PAYMENT_INITIATED', 'Card payment initiated, OTP sent', GETUTCDATE());

And the system displays OTP verification screen
Payment form transitions to OTP verification screen
OTP input field for 6-digit code is displayed
Mock message shown: "OTP sent to mobile ending in **45"
Timer shows OTP validity: "Valid for 5 minutes"
Booking status still remains "Pending" - NOT confirmed yet

Step 3: OTP Verification & Booking Confirmation
When I enter valid OTP "123456"
OTP input field validates for exactly 6 digits
Any 6-digit number except "000000" is accepted as valid OTP
Submit button becomes enabled after 6 digits entered

API Call - Verify OTP:
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

POST /api/CardPayment/verify-otp


Request:
{
  "paymentId": 456,
  "otp": "123456"
}


Response (200):
{
  "success": true,
  "data": {
    "isSuccess": true,
    "message": "Payment completed successfully",
    "paymentId": 456,
    "transactionId": "TXN1234567890",
    "cardType": "Visa",
    "cardLast4Digits": "1111",
    "cardHolderName": "John Doe"
  }
}

Internal Call Flow - After OTP Verification:
When OTP verification is successful, the system internally calls:
csharp
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

// File: CardPaymentService.cs - VerifyOtpAsync() method
public async Task<Result<CardPaymentFinalResponseDto>> VerifyOtpAsync(OtpVerificationDto request, CancellationToken cancellationToken = default)
{
    // Step 1: Validate OTP
    var otpResult = await ValidateOtpAsync(request.PaymentId, request.Otp);
    
    if (otpResult.IsSuccess && otpResult.Value)
    {
        // Step 2: Mark payment as active
        await _paymentRepository.MarkPaymentAsActiveAsync(request.PaymentId);
        
        // Step 3: 🎯 INTERNAL CALL - Confirm booking
        var confirmationDto = new BookingConfirmationDto
        {
            BookingId = paymentResult.Value.BookingId,
            PaymentReferenceId = paymentResult.Value.TransactionId,
            IsPaymentSuccessful = true
        };
        
        // 🎯 This internally executes sp_ConfirmBooking stored procedure
        var confirmBookingResult = await _busService.ConfirmBookingAsync(confirmationDto);
        
        if (confirmBookingResult.IsSuccess)
        {
            return new SuccessResult(cardPaymentDto);
        }
    }
}

Database Updates - Mark Payment Complete & Confirm Booking:
sql
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

-- Step 1: Update payment status to Success
UPDATE Payments 
SET PaymentStatus = 'Success', 
    IsActive = 1,
    PaymentDate = GETUTCDATE(),
    UpdatedBy = 'PaymentSystem',
    UpdatedOn = GETUTCDATE()
WHERE PaymentId = @PaymentId;


-- Step 2: Mark OTP as verified
UPDATE OtpLogs 
SET IsVerified = 1, 
    VerificationTime = GETUTCDATE()
WHERE PaymentId = @PaymentId;


-- Step 3: 🎯 INTERNAL CALL - Execute stored procedure to confirm booking
EXEC sp_ConfirmBooking 
    @BookingId = @BookingId,
    @PaymentReferenceId = @TransactionId,
    @IsPaymentSuccessful = 1;
    
-- Step 4: Update booking status to "Confirmed" (executed in sp_ConfirmBooking)
-- UPDATE Bookings 
-- SET BookingStatus = 'Confirmed', 
--     PaymentId = @PaymentId,
--     UpdatedAt = GETUTCDATE()
-- WHERE BookingId = @BookingId 
--   AND BookingStatus = 'Pending'
--   AND IsActive = 1;


-- Step 5: Lock seats permanently
-- UPDATE BookedSeats 
-- SET Status = 'Locked'
-- WHERE BookingId = @BookingId;


-- Create audit log for booking confirmation
INSERT INTO AuditLogs (BookingId, PaymentId, Action, Details, Timestamp)
VALUES (@BookingId, @PaymentId, 'BOOKING_CONFIRMED', 'Booking confirmed via successful OTP', GETUTCDATE());

What sp_ConfirmBooking Does Internally:
sql
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

CREATE PROCEDURE sp_ConfirmBooking
    @BookingId INT,
    @PaymentReferenceId NVARCHAR(50),
    @IsPaymentSuccessful BIT
AS
BEGIN
    -- 1. Validate booking exists and is pending
    SELECT BookingId, TotalAmount, Status, CustomerId
    FROM Bookings 
    WHERE BookingId = @BookingId 
      AND Status = 'Pending' 
      AND IsActive = 1;
    
    -- 2. Check if booking hasn't expired (10-minute window)
    IF DATEDIFF(MINUTE, CreatedOn, GETUTCDATE()) > 10
        THROW 50001, 'Booking has expired', 1;
    
    -- 3. Update booking status to "Confirmed"
    UPDATE Bookings 
    SET BookingStatus = 'Confirmed', 
        PaymentReferenceId = @PaymentReferenceId,
        UpdatedAt = GETUTCDATE()
    WHERE BookingId = @BookingId;
    
    -- 4. Lock all booked seats permanently
    UPDATE BookedSeats 
    SET Status = 'Locked'
    WHERE BookingId = @BookingId;
    
    -- 5. Return confirmation result
    SELECT 'Booking Confirmed' as Status, 
           @BookingId as BookingId, 
           GETUTCDATE() as ConfirmedAt;
END

Then the system should automatically confirm booking
This happens INTERNALLY without user action
No separate API call needed from frontend
System automatically executes sp_ConfirmBooking stored procedure
Payment record updated: PaymentStatus = "Success"
Booking record updated: BookingStatus = "Confirmed"
PaymentId linked to booking
All seats locked permanently

And the system displays "Payment Successful" with confirmation details
Success confirmation page displayed showing:


Success message: "Payment Successful"
Transaction ID: TXN1234567890
Booking confirmation details (BookingId, Bus, Route, Seats, Amount)
Confirmation timestamp
Option to download receipt or return to home page


Call Chain Sequence:
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

1. User enters OTP → CardPaymentController.VerifyOtp()
2. CardPaymentService.VerifyOtpAsync()
3. OTP validation passes ✅
4. Payment marked as active
5. 🎯 BusService.ConfirmBookingAsync() called INTERNALLY
6. Stored procedure sp_ConfirmBooking executed
7. Booking status changed from "Pending" → "Confirmed"
8. Seats locked permanently
9. Success response returned to frontend

Important:
✅ sp_ConfirmBooking is called ONLY after successful OTP verification
✅ Called internally from CardPaymentService.VerifyOtpAsync()
✅ No separate manual confirmation needed
❌ NOT called if OTP verification fails
❌ NOT called during payment initiation (Step 2)
❌ Only for Card Payments (UPI confirms directly)

Scenario 2: OTP Verification Failure
Given I have initiated card payment successfully
Payment record created with PaymentStatus = "Pending"
OTP sent and input field displayed
Timer showing validity period

When I enter invalid OTP "000000"
OTP input field validates for exactly 6 digits
Special test OTP "000000" is reserved for failure testing
Submit button is enabled

Then the system should reject payment
API returns failure response

API Response - Invalid OTP:
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

{
  "success": false,
  "error": "Invalid OTP. Payment failed."
}

And the system creates payment record with PaymentStatus = "Failed"
Payment record created/updated:


PaymentStatus = "Failed"
FailureReason = "Invalid OTP"
PaymentDate = Current Timestamp


Database Updates - Mark Payment Failed:
sql
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

-- Update payment to failed
UPDATE Payments 
SET PaymentStatus = 'Failed',
    UpdatedBy = 'OTPSystem',
    UpdatedOn = GETUTCDATE()
WHERE PaymentId = @PaymentId;


-- Log OTP failure
UPDATE OtpLogs 
SET IsVerified = 0, 
    VerificationAttempts = VerificationAttempts + 1,
    LastAttemptAt = GETUTCDATE()
WHERE PaymentId = @PaymentId;


-- Insert audit log
INSERT INTO AuditLogs (PaymentId, Action, Details, Timestamp)
VALUES (@PaymentId, 'OTP_VERIFICATION_FAILED', 'Invalid OTP entered', GETUTCDATE());

And error message is displayed: "Invalid OTP. Payment failed"
Clear error message indicating failure reason
Actionable guidance for user provided

And booking status remains as "Pending"
Booking record not updated
BookingStatus = "Pending" maintained
Seat selection held for grace period (15 minutes)

And system shows retry payment option
"Retry Payment" button displayed prominently
User can attempt payment again without re-selecting seats
Original booking details preserved

Scenario 3: Failed Payment (Test Card: 4000000000000002)
Given I am on the payment page with valid pending booking
System verifies booking exists with BookingStatus = "Pending"
Payment Page displays all required information

When I enter the test card number "4000000000000002"
System recognizes this as predefined failure test card
All field validations pass (16 digits, valid format)
Card type detected and displayed

And I enter valid expiry date, CVV, and submit payment
All input validation passes successfully
Submit button clicked
Form fields disabled and processing state displayed
3-second simulated processing delay executed

Then the system should simulate payment failure
Mock payment service processes request and returns failure after delay
Payment processing completes with failure status

API Response - Payment Failed:
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

{
  "success": false,
  "error": "Payment failed: Insufficient funds"
}

Database Updates - Failed Payment:
sql
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

-- Create failed payment record
INSERT INTO Payments (BookingId, Amount, PaymentMethod, PaymentStatus, FailureReason, TransactionId, CreatedBy, CreatedOn)
VALUES (@BookingId, @Amount, 'DebitCard', 'Failed', 'Insufficient Funds', @TransactionId, 'System', GETUTCDATE());


-- Check retry count
SELECT COUNT(*) as FailedAttempts
FROM Payments 
WHERE BookingId = @BookingId 
  AND PaymentStatus = 'Failed';


-- Log failure
INSERT INTO AuditLogs (PaymentId, Action, Details, Timestamp)
VALUES (@PaymentId, 'PAYMENT_FAILED', 'Test failure card used', GETUTCDATE());

And system displays "Payment Failed - Insufficient Funds"
Error message displayed clearly with failure reason
Actionable guidance provided

And booking status remains as "Pending"
Booking record not updated
BookingStatus = "Pending" retained
Seat selection held for 15 minutes

And system shows retry payment option
"Retry Payment" button displayed prominently
Original booking details preserved
Can attempt payment again without re-selecting seats

Scenario 4: Session Timeout Handling
Given I am on the payment page
Payment form is loaded and ready for input
Session timer has started

When I remain inactive for 10 minutes
User does not interact with the page
No mouse movement, clicks, or keyboard input
10 minutes (600 seconds) elapse without interaction

Then the system should timeout the session
Payment session automatically terminated
Payment form cleared from memory
Session state reset

Database Updates - Mark Timeout:
sql
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

-- Mark payment as timeout
UPDATE Payments 
SET PaymentStatus = 'Timeout',
    UpdatedBy = 'SessionManager',
    UpdatedOn = GETUTCDATE()
WHERE PaymentId = @PaymentId 
  AND PaymentStatus = 'Pending';


-- Clear OTP
UPDATE OtpLogs 
SET IsActive = 0
WHERE PaymentId = @PaymentId 
  AND GETUTCDATE() > ExpiresAt;

And system displays "Session expired for security" message
Modal or alert displays: "Your session has expired for security reasons"
Explanation: "Please start the payment process again"

And booking status remains "Pending"
Booking record retained with BookingStatus = "Pending"
Seats still held if within grace period
User must re-enter payment details to retry

Scenario 5: Mock Card Type Detection and Validation
Given I am on the payment page
Payment form loaded and ready for input
Card number input field active and empty

When I enter a card number starting with "4" (Visa)
System detects first digit as "4"
Real-time card type detection triggered

Then I should see "Visa" card type indicator
Visa logo or "Visa" text label displayed
Indicator updates in real-time as digits entered

And system should validate using Visa rules
Card number exactly 16 digits
Luhn algorithm validation applied
CVV must be exactly 3 digits

When I enter a card number starting with "5" (MasterCard)
System detects first digit as "5"
Card type indicator updates dynamically

Then I should see "MasterCard" card type indicator
MasterCard logo or text label displayed
MasterCard-specific validation rules applied

When I enter a card number starting with "6" (RuPay)
System detects first digit as "6"
Card type indicator updates to RuPay

Then I should see "RuPay" card type indicator
RuPay logo or text label displayed
RuPay-specific validation rules applied

And system should store card type as "Debit Card - Visa"
Upon successful payment, Payments table record includes:


PaymentMethod = "Debit Card - [CardType]"
Stored for reporting and analytics purposes


US-PM-002: Mock UPI Payment
Priority: High | Story Points: 8 | Sprint: 3
User Story
As a customer
I want to simulate UPI payments
So that I can test the UPI payment flow with a mock
Preconditions
Customer completed bus and seat selection and reached the Payment Page
Booking record exists with BookingStatus = "Pending"
Mock payment environment is active
UPI payment option is available and displayed on the Payment Page

Acceptance Criteria
Scenario 1: Mock UPI ID Payment
Given I am on the payment page with a valid pending booking
The system verifies that a booking exists with BookingStatus = "Pending"
Payment options including UPI are displayed

When I select the UPI payment option
The UPI payment interface is displayed
An input field for UPI ID is shown
Format guidance is provided (e.g., "username@bank")

And I enter a UPI ID in the format "test@mockupi"
The input field accepts alphanumeric characters, "@", and "."
Validation ensures format matches: [username]@[provider]
The UPI ID "test@mockupi" is recognized as a valid test ID

Then the system should show "Connecting to UPI app" for 2 seconds
A loading indicator is displayed with message: "Connecting to UPI app"
The simulated connection delay is exactly 2000 ms
User interface shows processing state with appropriate visual feedback

And the system creates a payment record with PaymentMethod = "UPI"
A new record is inserted into the Payments table with:


PaymentMethod = "UPI"
PaymentStatus = "Completed"
Auto-generated PaymentId
PaymentDate = Current Timestamp
Amount = Total fare from booking
Database Updates - Mark Payment Complete & Confirm Booking:
sql
-- Step 1: Update payment status to SuccessUPDATE Payments SET PaymentStatus = 'Success',     IsActive = 1,    PaymentDate = GETUTCDATE(),    UpdatedBy = 'PaymentSystem',    UpdatedOn = GETUTCDATE()WHERE PaymentId = @PaymentId;-- Step 2: Mark OTP as verifiedUPDATE OtpLogs SET IsVerified = 1,     VerificationTime = GETUTCDATE()WHERE PaymentId = @PaymentId;-- Step 3: 🎯 INTERNAL CALL - Execute stored procedure to confirm bookingEXEC sp_ConfirmBooking     @BookingId = @BookingId,    @PaymentReferenceId = @TransactionId,    @IsPaymentSuccessful = 1;    -- Step 4: Update booking status to "Confirmed" (executed in sp_ConfirmBooking)-- UPDATE Bookings -- SET BookingStatus = 'Confirmed', --     PaymentId = @PaymentId,--     UpdatedAt = GETUTCDATE()-- WHERE BookingId = @BookingId --   AND BookingStatus = 'Pending'--   AND IsActive = 1;-- Step 5: Lock seats permanently-- UPDATE BookedSeats -- SET Status = 'Locked'-- WHERE BookingId = @BookingId;-- Create audit log for booking confirmationINSERT INTO AuditLogs (BookingId, PaymentId, Action, Details, Timestamp)VALUES (@BookingId, @PaymentId, 'BOOKING_CONFIRMED', 'Booking confirmed via successful OTP', GETUTCDATE());
What sp_ConfirmBooking Does Internally:
sql
CREATE PROCEDURE sp_ConfirmBooking    @BookingId INT,    @PaymentReferenceId NVARCHAR(50),    @IsPaymentSuccessful BITASBEGIN    -- 1. Validate booking exists and is pending    SELECT BookingId, TotalAmount, Status, CustomerId    FROM Bookings     WHERE BookingId = @BookingId       AND Status = 'Pending'       AND IsActive = 1;        -- 2. Check if booking hasn't expired (10-minute window)    IF DATEDIFF(MINUTE, CreatedOn, GETUTCDATE()) > 10        THROW 50001, 'Booking has expired', 1;        -- 3. Update booking status to "Confirmed"    UPDATE Bookings     SET BookingStatus = 'Confirmed',         PaymentReferenceId = @PaymentReferenceId,        UpdatedAt = GETUTCDATE()    WHERE BookingId = @BookingId;        -- 4. Lock all booked seats permanently    UPDATE BookedSeats     SET Status = 'Locked'    WHERE BookingId = @BookingId;        -- 5. Return confirmation result    SELECT 'Booking Confirmed' as Status,            @BookingId as BookingId,            GETUTCDATE() as ConfirmedAt;END
Then the system should automatically confirm booking
This happens INTERNALLY without user action
No separate API call needed from frontend
System automatically executes sp_ConfirmBooking stored procedure
Payment record updated: PaymentStatus = "Success"
Booking record updated: BookingStatus = "Confirmed"
PaymentId linked to booking
All seats locked permanently
And the system displays "Payment Successful" with confirmation details
Success confirmation page displayed showing:


Success message: "Payment Successful"
Transaction ID: TXN1234567890
Booking confirmation details (BookingId, Bus, Route, Seats, Amount)
Confirmation timestamp
Option to download receipt or return to home page



And the booking is confirmed
The booking record is updated with:


BookingStatus = "Confirmed"
PaymentId linked to the payment record
UpdatedAt = Current Timestamp

Success confirmation is displayed with transaction detail

Scenario 3: Mock UPI Payment Timeout
Given I have initiated a UPI payment
The user has selected UPI payment method
UPI ID has been entered 
Payment session has been started

When I don't complete the payment within 5 minutes
The system tracks elapsed time from UPI payment initiation
After 5 minutes (300 seconds) without completion, timeout is triggered
No payment confirmation is received within the timeout period

Then the system should timeout the session
The payment session is automatically terminated
The payment interface displays a timeout message

And the system creates a payment record with PaymentStatus = "Timeout"
A record is inserted into the Payments table with:


PaymentStatus = "Timeout"
PaymentMethod = "UPI"
Auto-generated PaymentId
PaymentDate = Current Timestamp
Notes indicating session timeout


And the system shows a retry payment option
An error message is displayed: "Payment session timed out. Please try again"
A "Retry Payment" button is provided
Alternative payment methods are suggested

And the booking status remains as "Pending"
The booking record retains BookingStatus = "Pending"
Seat hold is maintained for a grace period
User can retry payment without losing seat selection

Scenario 4: Mock UPI Failure Scenarios
Given I am on the UPI payment page
The system displays the UPI payment interface
UPI payment option is active

When I enter the UPI ID "fail@test"
The user enters the predefined failure test UPI ID: "fail@test"
The system recognizes this as a test ID for failure simulation
UPI ID format validation passes

Then the system should simulate UPI failure
After simulated processing delay, the system returns a failure response
Failure is triggered by the specific test UPI ID

And the system creates a payment record with PaymentStatus = "Failed"
A record is inserted into the Payments table with:


PaymentStatus = "Failed"
PaymentMethod = "UPI"
Auto-generated PaymentId
PaymentDate = Current Timestamp
Failure reason stored for audit


And the booking status remains "Pending"
The booking record retains BookingStatus = "Pending"
User can retry payment with a different UPI ID or payment method
Seat hold is maintained for the grace period

US-PM-003: Mock Payment Status Tracking
Priority: Medium | Story Points: 5 | Sprint: 4
User Story
As a customer
I want to see simulated real-time payment status updates
So that I can test the status tracking functionality and understand payment progress
Preconditions
A payment transaction has been initiated
Payment record exists in the Payments table
User is on the payment processing or status page

Acceptance Criteria
Scenario 1: Mock Real-Time Status Updates
Given I have initiated a mock payment
The user has submitted payment details (card or UPI)
A payment record has been created in the Payments table with initial status
The payment processing interface is displayed

When the payment is processing
The mock payment service simulates payment gateway processing
Database updates occur at simulated intervals

Then I should see status progression: "Initiated" → "Processing" → "Completed"
The UI displays the current status in real-time:


Initial status: "Payment Initiated"
After 2 seconds: "Processing Payment"
After another 2 seconds: "Payment Completed" (for successful payments)

Each status change is accompanied by appropriate visual indicators (icons, progress bar)

And PaymentStatus in the database should update accordingly
The Payments table record is updated at each status change:


First: PaymentStatus = "Initiated"
Then: PaymentStatus = "Processing"
Finally: PaymentStatus = "Completed" or "Failed"

Timestamp fields are updated with each status change

And status should change every 2 seconds during simulation
The simulated processing time between status changes is 2000 ms
Total processing time from "Initiated" to final status is approximately 4-6 seconds
Timing simulates realistic payment gateway behavior

And final status should persist in the Payments table
Once the final status ("Completed" or "Failed") is reached, no further status changes occur
The PaymentStatus field retains the final value permanently
The final status is used for reporting, reconciliation, and audit purposes

Scenario 2: Mock Payment Completion Notification
Given my mock payment is processing
Payment status is currently "Processing"
The user is viewing the payment status page
Database record shows PaymentStatus = "Processing"

When the simulation completes successfully
The mock payment service completes processing
All validation and processing steps return success
Final status determination is made

Then I should see "Payment Successful" message
The UI displays a prominent success message: "Payment Successful"
Visual indicators show success (green checkmark icon, success banner)
Transaction details are displayed (Transaction ID, amount, timestamp)

And PaymentStatus should be "Completed" in the database
The Payments table record is updated with:


PaymentStatus = "Completed"
PaymentDate = Current Timestamp (completion time)


And booking status should update to "Confirmed"
The corresponding Bookings table record is updated:


BookingStatus = "Confirmed"
PaymentId is linked
UpdatedAt = Current Timestamp


And PaymentDate should be set to current timestamp
The PaymentDate field in the Payments table records the exact completion time
This timestamp is used for reporting and confirmation communications

US-PM-004: Mock Payment Confirmation
Priority: High | Story Points: 5 | Sprint: 4
User Story
As a customer
I want to receive simulated payment confirmations
So that I can test the confirmation delivery system with sending actual email
Preconditions
A payment transaction has been completed successfully
Payment record exists with PaymentStatus = "Completed"
Customer information (email, phone) is available in the system
Mock notification service is configured

Acceptance Criteria
Scenario 1: Mock Successful Payment Confirmation
Given I have completed a mock successful payment
Payment record exists in the Payments table with PaymentStatus = "Completed"
Booking status has been updated to "Confirmed"
All transaction details are available (BookingId, PaymentId, Amount)

When the PaymentStatus is set to "Completed"
The payment completion event triggers the confirmation workflow
The system retrieves customer email information from the database

Then the system should log "Email sent to [user.email]" in console
Console output displays: "Email sent to customer@example.com"
Log entry includes timestamp and email address
Log confirms that email notification would be sent in production

And confirmation should include BookingId, PaymentId, and Amount
Console logs display all key transaction details:


BookingId: BK20251009-01
PaymentId: PAY-MOCK-12345
Amount: ₹1,250.00

These details would be included in actual email

And I should see "Confirmation sent to email" message on screen
The UI displays a confirmation message: "Confirmation sent to your email"
The message provides confidence that notifications were triggered
Confirmation details are displayed on the success page
Email address provided during booking process is shown

🎯 EPIC 4: BOOKING CANCELLATION & REFUND MANAGEMENT
Epic Description
Enable customers to cancel confirmed bookings based on cancellation policies and automatically process refunds according to timing rules. Provide complete refund tracking and management.
Feature 1: Booking Cancellation with Refund
US-BRC-001: Cancel Booking and Request Refund
Priority: High | Story Points: 13 | Sprint: 5
User Story
As a customer
I want to cancel my booking and request a refund
So that I can get my money back according to the cancellation policy
Preconditions
A confirmed booking exists with BookingStatus = "Confirmed"
Associated payment record exists with PaymentStatus = "Completed"
Cancellation policies are defined in the system
User has access to cancel booking functionality
Departure date/time is recorded in the booking

Refund Policy Rules
Plain text
ANTLR4
Bash
C
C#
CSS
CoffeeScript
CMake
Dart
Django
Docker
EJS
Erlang
Git
Go
GraphQL
Groovy
HTML
Java
JavaScript
JSON
JSX
Kotlin
LaTeX
Less
Lua
Makefile
Markdown
MATLAB
Markup
Objective-C
Perl
PHP
PowerShell
.properties
Protocol Buffers
Python
R
Ruby
Sass (Sass)
Sass (Scss)
Scheme
SQL
Shell
Swift
SVG
TSX
TypeScript
WebAssembly
YAML
XML

Time Before Departure | Refund Amount | Penalty | Scenario
─────────────────────────────────────────────────────────────
> 24 hours           | 100% (Full)   | ₹0     | Early Cancellation
12-24 hours          | 50% (Half)    | 50%    | Late Cancellation
< 12 hours           | 0% (None)     | 100%   | Very Late/No Refund

Acceptance Criteria
Scenario 1: Full Refund (Cancellation > 24 hours before departure)
Given I have a confirmed booking with PaymentStatus = "Completed"
Booking exists in database with:


BookingStatus = "Confirmed"
Associated PaymentId with PaymentStatus = "Completed"
DepartureDateTime is set to a future date/time

Original payment amount is recorded in Payments table

When I cancel more than 24 hours before departure
User initiates cancellation process from booking details page
System calculates time difference between current time and departure time
Time difference is greater than 24 hours
Cancellation is confirmed by the user

Then the system should calculate 100% refund of original Amount
Refund calculation: RefundAmount = Payment.Amount × 1.0
Example: If payment was ₹1,250, refund = ₹1,250
Calculation is performed server-side to ensure accuracy

And the system creates a record in Refunds table with RefundStatus = "Initiated"
A new record is inserted into the Refunds table:


RefundId: Auto-generated unique identifier
PaymentId: Linked to original payment
BookingId: Linked to cancelled booking
RefundAmount: ₹1,250 (100% of payment)
RefundStatus: "Initiated"
RefundMethod: Same as original PaymentMethod
InitiatedOn: Current timestamp
RefundedOn: NULL (to be updated on completion)


And the system creates a Cancellations record with PenaltyAmount = 0
A new record is inserted into the Cancellations table:


CancellationId: Auto-generated unique identifier
BookingId: Linked to cancelled booking
CancellationDate: Current timestamp
CancellationReason: User-provided reason (optional)
PenaltyAmount: ₹0 (no penalty for early cancellation)
RefundId: Linked to the newly created refund record


And the system displays "Full refund of ₹[amount] initiated"
A success message is displayed: "Full refund of ₹1,250 initiated"
The message includes expected refund timeline based on payment method
Booking status is updated to "Cancelled"
User is redirected to cancellation confirmation page

Scenario 2: Partial Refund (Cancellation 12-24 hours before departure)
Given I have a confirmed booking with completed payment
Booking exists with BookingStatus = "Confirmed"
Payment record has PaymentStatus = "Completed"
Original payment amount is available

When I cancel between 12-24 hours before departure
User initiates cancellation
System calculates time difference: 12 hours < time_until_departure < 24 hours
Partial refund policy applies
User confirms cancellation after reviewing penalty

Then the system should calculate 50% refund of payment Amount
Refund calculation: RefundAmount = Payment.Amount × 0.5
Example: If payment was ₹1,250, refund = ₹625
Penalty calculation: PenaltyAmount = Payment.Amount × 0.5 = ₹625

And the system creates a Refunds record with RefundAmount = Amount × 0.5
A new record is inserted into the Refunds table:


RefundAmount: ₹625 (50% of ₹1,250)
RefundStatus: "Initiated"
RefundMethod: Same as original payment method
PaymentId and BookingId are linked


And the system creates a Cancellations record with PenaltyAmount = Amount × 0.5
A new record is inserted into the Cancellations table:


PenaltyAmount: ₹625 (50% of ₹1,250)
BookingId: Linked to cancelled booking
RefundId: Linked to refund record
CancellationDate: Current timestamp


And RefundStatus is set to "Initiated"
The Refunds table record has initial status: "Initiated"
Status will progress through "Processing" to "Completed" in simulation

And the system shows calculated refund amount before confirmation
Before finalizing cancellation, a confirmation dialog displays:


"Cancellation Penalty: ₹625 (50%)"
"Refund Amount: ₹625 (50%)"
"Total Paid: ₹1,250"

User must confirm after reviewing the breakdown
Provides transparency in refund calculation

Scenario 3: No Refund Policy (Cancellation < 12 hours before departure)
Given I have a confirmed booking with completed payment
Booking exists with BookingStatus = "Confirmed"
Payment record has PaymentStatus = "Completed"
Departure time is approaching

When I try to cancel less than 12 hours before departure
User initiates cancellation process
System calculates time difference: time_until_departure < 12 hours
No refund policy applies per cancellation rules

Then the system should show "No refund applicable" message
A clear warning message is displayed: "No refund applicable for cancellations within 12 hours of departure"
User is informed before confirming cancellation
Full penalty amount is shown

And the system creates a Cancellations record with PenaltyAmount equal to full Amount
A new record is inserted into the Cancellations table:


PenaltyAmount: ₹1,250 (100% of payment - full forfeiture)
BookingId: Linked to cancelled booking
RefundId: NULL (no refund record created)
CancellationDate: Current timestamp


And the system should not create any record in the Refunds table
No entry is made in the Refunds table
RefundId in Cancellations record remains NULL
Payment amount is fully forfeited as penalty

And booking should still be cancelled but with no refund
Booking status is updated to "Cancelled"
Seats are released back to inventory
User receives cancellation confirmation with zero refund
Payment record retains PaymentStatus = "Completed" (no refund processed)

Feature 2: Refund Processing & Status Tracking
US-BRC-002: Process Refunds with Status Tracking
Priority: High | Story Points: 8 | Sprint: 5
User Story
As a customer
I want to track the status of my refund
So that I can test refund monitoring functionality and understand refund progress
Preconditions
A refund has been initiated and a record exists in the Refunds table
User has access to booking details or refund tracking page
Refund is associated with a cancelled booking

Acceptance Criteria
Scenario 1: Mock Refund Status Display
Given I have a refund record in the Refunds table
A refund record exists with a valid RefundId
Record contains RefundAmount, RefundMethod, and current RefundStatus
User is viewing booking details or refund tracking page

When I check my booking details
User navigates to the specific booking's detail page
System queries the Refunds table using BookingId
Refund information is retrieved and displayed

Then I should see current RefundStatus from the database
The current status is prominently displayed:


"Refund Initiated"
"Refund Processing"
"Refund Completed"

Status is retrieved directly from the RefundStatus field
Visual indicator (icon, badge, progress bar) shows status

And I should see RefundAmount and RefundMethod
Refund details displayed:


"Refund Amount: ₹625"
"Refund Method: Debit Card - Visa"

Information is clearly formatted and easy to understand

And I should see simulated timeline based on RefundMethod
Expected timeline is displayed based on refund method:


UPI: "Refund typically processed in 1-2 business days"
Debit/Credit Card: "Refund typically processed in 3-5 business days"
Net Banking: "Refund typically processed in 3-7 business days"

Timeline is informational and based on typical processing times

And timeline should show "1-2 days for UPI" or "3-5 days for cards"
Specific messaging based on RefundMethod:


If RefundMethod contains "UPI": Display "Expected within 1-2 business days"
If RefundMethod contains "Card": Display "Expected within 3-5 business days"

Timeline starts from the refund initiation date

Scenario 2: Mock Refund Completion Notification
Given my RefundStatus is "Processing"
Refund record exists with RefundStatus = "Processing"
Mock refund simulation is in progress
User may be viewing the refund status page or not

When the system simulates refund completion after delay
After the simulated processing delay (e.g., 5 seconds)
Refund processing simulation determines completion

Then RefundStatus should update to "Completed"
The Refunds table record is updated:


RefundStatus = "Completed"
Database update occurs automatically


And RefundedOn timestamp should be set
The RefundedOn field is populated with current timestamp
Example: RefundedOn = "2025-10-09 15:45:30"
This marks the exact completion time of the refund

And the system should log "Refund completed notification sent"
Console output displays: "Refund completed notification sent to customer@example.com"
Log entry includes:


RefundId
Customer email/phone
Completion timestamp

Simulates notification trigger (actual notification not sent in mock environment)

And I should see "Refund of ₹[amount] completed" message
If user is viewing the page, a success message appears:


"Refund of ₹625 completed successfully"
"Amount has been credited to your Debit Card - Visa"

Message may appear as banner, modal, or inline notification
Provides immediate feedback on refund completion

Scenario 3: Mock Refund Timeline Display
Given I have refunds with different RefundMethod values
Multiple refund records exist with varying refund methods:


Some refunds via UPI
Some refunds via Debit Card
Some refunds via Credit Card

User is viewing refund details for a specific booking

When I view refund details
User navigates to a booking with an associated refund
System retrieves refund record from Refunds table
RefundMethod field determines the timeline display

Then UPI refunds should show "Expected in 1-2 business days"
For refunds where RefundMethod contains "UPI":


Display message: "Expected in 1-2 business days"
Timeline is calculated from InitiatedOn date
Business days exclude weekends and holidays (optional for mock)


And Card refunds should show "Expected in 3-5 business days"
For refunds where RefundMethod contains "Card" (Debit or Credit):


Display message: "Expected in 3-5 business days"
Timeline provides realistic expectation setting


And timeline should be calculated from PaymentDate of original payment
Timeline calculation starts from when refund was initiated
Display shows:


"Refund initiated on: October 9, 2025"
"Expected completion: October 12-14, 2025" (for cards)

Calculation considers the RefundMethod-specific processing time

And actual RefundedOn date should be shown when available
Once RefundStatus = "Completed" and RefundedOn is set:


Display: "Refund completed on: October 11, 2025"
Actual completion date replaces expected timeline
Shows precise completion timestamp if needed


US-BRC-003: Mock Automatic Refund Processing
Priority: High | Story Points: 8 | Sprint: 5
User Story
As a customer
I want to simulate automatic refund processing based on cancellation timing
So that I can test refund calculation and processing functionality without actual money transfers
Acceptance Criteria
Scenario 1: Mock Refund Processing Simulation
Given I have initiated a mock refund
A refund record exists in the Refunds table
Initial RefundStatus = "Initiated"
Refund amount and method are set

When RefundStatus is "Initiated"
Mock refund processing simulation begins

Then the system should simulate processing delay of 5 seconds
A 5000 ms delay is enforced to simulate real-world processing time
During this delay, a loading indicator is displayed
Status message shows: "Processing refund..."

And the system updates RefundStatus to "Processing" then "Completed"
Status progression:


At 0 seconds: RefundStatus = "Initiated"
At 2 seconds: RefundStatus = "Processing"
At 5 seconds: RefundStatus = "Completed"

Database updates occur at each status transition
UI reflects current status in real-time

And the system sets RefundedOn to current timestamp
When final status "Completed" is reached:


RefundedOn field is updated with current timestamp
This timestamp represents the completion of the refund
Used for reporting and tracking purposes


And the system logs "Refund of ₹[amount] processed to [method]"
Console output displays: "Refund of ₹625 processed to Debit Card - Visa"
Log entry includes:


RefundId
Refund amount
Refund method
Completion timestamp

Log confirms successful refund simulation

🎯 EPIC 5: CUSTOMER REVIEW & RATING SYSTEM
Epic Description
Enable customers to review and rate completed trips, providing valuable feedback for service improvement and helping other customers make informed decisions.
Feature 1: Add Review & Rating
US-CR-001: Review and Rate Completed Trip
Priority: Medium | Story Points: 8 | Sprint: 6
User Story
As a customer who completed a bus journey
I want to review and rate my trip experience
So that I can share feedback and help other customers make informed booking decisions
Preconditions
Booking status = "Confirmed"
Travel date has passed (trip completed)
Booking is not cancelled
User is logged in
User hasn't already rated this trip

Acceptance Criteria
Scenario 1: Navigate to Review Form
Given I have completed a bus journey
The booking shows TravelDate < CurrentDate
BookingStatus = "Confirmed"
Trip is marked as "Completed"

When I view my trip in "Others" → "Completed Trips"
The trip card displays: "✅ COMPLETED"
An action button is visible: "⭐ Rate Trip"

When I click "⭐ Rate Trip"
The system verifies I can rate this trip (not already rated, trip is completed)
A review modal or page opens
The system fetches trip details for context

Then the review form should display:
Trip details: Bus name, route, date, seats booked
Star rating selector (1-5 stars)
Text input for written review (optional, max 1000 chars)
Individual rating categories (if applicable): Cleanliness, Safety, Comfort, Driving
Submit and Cancel buttons

Scenario 2: Submit Star Rating
Given the review form is open
I can see the 5-star rating selector
Currently no stars are selected

When I click on the 4th star
All stars up to the 4th star are highlighted (filled)
The selected rating displays: "⭐⭐⭐⭐ Good (4/5)"
The rating is saved temporarily in the form

Then when I click Submit
The system validates that a rating is selected
POST /api/review/submit is called with:


BookingId
Rating: 4
ReviewText: (optional)

Upon success:


✅ "Thank you for your review!"
User is redirected to My Trips
Review appears on the trip card


Scenario 3: Write Review Text
Given the review form is open with a star rating selected
I have selected 5 stars: "⭐⭐⭐⭐⭐ Excellent (5/5)"

When I click on the "Write your review" text area
A text input field expands
Placeholder text: "Share your experience with this bus journey..."
Character counter shows: "0/1000"

And I type: "Great bus service! Comfortable seats, good timing, friendly staff. Would book again."
Text is entered in real-time
Character counter updates: "95/1000"
No validation errors

When I click Submit
The system validates:


Rating is selected: ✓
Review text is optional but if provided, must be 10-1000 chars

ReviewText = "Great bus service! Comfortable seats..."
POST /api/review/submit is called with all data
Review is saved to Reviews table

Then confirmation is displayed:
✅ "Review submitted successfully!"
"Your review helps other travelers. Thank you!"
Trip card now shows: "⭐⭐⭐⭐⭐ (You rated this 5 stars)"

Scenario 4: Rate Individual Aspects (Optional)
Given the review form is open
Additional rating categories are displayed:


Cleanliness (1-5 stars)
Safety (1-5 stars)
Comfort (1-5 stars)
Driving (1-5 stars)


When I rate each category
I click stars for "Cleanliness": ⭐⭐⭐⭐ (4 stars)
I click stars for "Safety": ⭐⭐⭐⭐⭐ (5 stars)
I click stars for "Comfort": ⭐⭐⭐ (3 stars)
I click stars for "Driving": ⭐⭐⭐⭐ (4 stars)

Then these ratings should be saved
Individual ratings are stored in Reviews table:


Cleanliness: 4
Safety: 5
Comfort: 3
Driving: 4

Overall rating is calculated as average: (4+5+3+4)/4 = 4

Scenario 5: View Submitted Review
Given I have submitted a review for a trip
Review record exists in Reviews table
ReviewStatus = "Active" (approved or auto-approved)

When I navigate to "My Trips" → "Completed Trips"
The trip card displays my review summary:


"⭐⭐⭐⭐ You rated this 4 stars"
Option to edit or delete review
"[✎ Edit Review] [🗑️ Delete]" buttons


When I click on the trip to view details
The full review is displayed:


Star rating with count
Review text
Individual category ratings (if submitted)
Submit date


Scenario 6: Edit or Delete Review
Given I have submitted a review
The review is visible on the trip card
I have edit/delete options

When I click "✎ Edit Review"
The review form reopens with pre-filled data:


Star rating: 4 (highlighted)
Review text: "Great bus service!..."
Individual ratings (if submitted)


And I change the rating to 5 stars
I click the 5th star: ⭐⭐⭐⭐⭐
The form updates the rating to 5

When I click Submit
PUT /api/review/{reviewId} is called with updated data
✅ "Review updated successfully!"
The trip card now shows: "⭐⭐⭐⭐⭐ Updated"

When I click "🗑️ Delete"
A confirmation dialog appears: "Delete this review?"
"Your review will be permanently removed"
[Confirm] [Cancel]

And I click Confirm
DELETE /api/review/{reviewId} is called
The review is removed from the system
The trip card no longer shows a review
✅ "Review deleted successfully!"

Scenario 7: Error Handling
Given the review form is open
When I try to submit without selecting a rating
Error message: "Please select a star rating (1-5)"
Submit button remains disabled

When I try to submit with review text < 10 characters
Error message: "Review must be at least 10 characters"
Submit button remains disabled

When the API call fails
Error message: "Unable to submit review. Please try again"
[Retry] [Go Back] buttons shown

US-CR-002: View Bus Reviews
Priority: Medium | Story Points: 5 | Sprint: 6
User Story
As a prospective customer browsing buses
I want to see reviews and ratings from other customers
So that I can make informed decisions when booking
Preconditions
Bus has at least one review
Reviews are public and visible
User is on bus details page

Acceptance Criteria
Scenario 1: Display Bus Review Summary
Given I am viewing a bus on the search/details page
The bus card displays basic info: Bus name, route, price, seats

When the reviews section loads
A "Reviews" section appears on the bus card/page
Overall rating displayed: "⭐⭐⭐⭐ 4.2 out of 5"
Total review count: "(1,245 reviews)"
Rating breakdown shown:


⭐⭐⭐⭐⭐ 5 stars: 500 reviews
⭐⭐⭐⭐ 4 stars: 400 reviews
⭐⭐⭐ 3 stars: 200 reviews
⭐⭐ 2 stars: 100 reviews
⭐ 1 star: 45 reviews


Scenario 2: View Individual Reviews
Given I click "View All Reviews" or "(1,245 reviews)"
A reviews page or modal opens
Reviews are displayed in a paginated list (10 per page)
Each review shows:


Star rating (⭐⭐⭐⭐)
Reviewer name (or "Anonymous" if anonymous)
Review text: "Great bus service! Comfortable seats..."
Date: "Reviewed on: 28 Oct 2025"
Individual ratings: Cleanliness: ⭐⭐⭐⭐, Safety: ⭐⭐⭐⭐⭐, etc.


Scenario 3: Filter and Sort Reviews
Given I am viewing the reviews list
Filter options appear:


Filter by rating: All, 5 stars, 4 stars, 3 stars, 2 stars, 1 star
Sort options: Most Recent, Highest Rating, Lowest Rating, Most Helpful


When I select "5 stars" filter
Only 5-star reviews are displayed
Count updates: "Showing 500 of 1,245 reviews"

When I select "Most Helpful" sort
Reviews with most helpful votes appear first

Scenario 4: Mark Review as Helpful
Given I am reading a customer review
Each review has a "👍 Helpful?" option with a counter

When I click "👍 Helpful"
The helpful count increments
My vote is recorded (prevents duplicate votes)
"👍 5 people found this helpful" displays



API Endpoints Summary
Profile Management (EPIC 1)
GET /api/customer/{customerId} - Fetch profile
PUT /api/customer/update - Update profile
PUT /api/customer/profile-picture - Upload picture

My Trips (EPIC 2)
GET /api/trips/upcoming/{customerId} - Upcoming trips
GET /api/trips/others/{customerId} - Other trips
GET /api/trips/details/{bookingId} - Trip details
POST /api/trips/cancel/{bookingId} - Cancel booking
GET /api/trips/ticket/{bookingId} - Download ticket

Payment Processing (EPIC 3)
GET /api/payment/init/{bookingId} - Initialize payment
POST /api/payment/process-debit-card - Process card (3-sec delay)
POST /api/payment/verify-otp - Verify OTP
POST /api/payment/process-upi - Process UPI (2-sec delay)
GET /api/payment/{paymentId}/status - Check payment status

Cancellation & Refund (EPIC 4)
GET /api/cancellation/check-eligibility/{bookingId} - Check cancellation eligibility
POST /api/cancellation/cancel - Cancel booking
GET /api/refund/{refundId}/status - Track refund

Reviews & Rating (EPIC 5)
POST /api/review/submit - Submit review
GET /api/review/{bookingId} - Get review
PUT /api/review/{reviewId} - Edit review
DELETE /api/review/{reviewId} - Delete review
GET /api/bus/{busId}/reviews - Get bus reviews








