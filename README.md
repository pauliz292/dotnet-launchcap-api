Aquila Clinic System
is a Clinic Information System able to manage patient data as well as receive and schedule bookings for an Appointment, or Consultation. The system will have 2 main portals, The BOOKING where users can make or book  an appointment to the clinic and monitor the booking status. The ADMIN for clinic personnel use where they can manage the Clinic's data


I - Booking (USER)

* Book an Appointment  
	The user should be able to book or schedule an appointment to the clinic.
* List all Booking   
	List of all booking made by the user, there should be list by date (recent) or status (PENDING, CONFIRMED,  UNAVAILABLE, CANCELED, CLOSED)
* check status of booking   
	will show the status (history) of the booking made by the user
* cancel previous booking made   
	the user should be able to make a cancelation request if the booking is not yet confirmed by the 	clinic


II - Clinic Information Management (ADMIN)

A.  Booking management

* List all Booking   
 	List of all booking received from patients, there should be a list by date (recent) or status  (PENDING, CONFIRMED,  UNAVAILABLE, CANCELED, CLOSED)
* Update Booking status(id)   
	 Clinic personnel should be able to update a booking status (PENDING, CONFIRMED,  UNAVAILABLE, CANCELED, CLOSED). It is important that when confirming bookings, there should be no conflicting date or time.   
	 A booking can have multiple status to track a "transition history" but will mainly use the most recent.  
* Closing  
	Clinic personnel should have the ability to close all bookings (for the day). This is to accommodate unattended bookings at the end of the day that might conflict to the next day transactions.  
	ALL Bookings should be CLOSED  

B.  Consultation Appointment Management

* Create new consultation appointment  
	This will initialize a new "consultation" with the patient. this can be from a booked appointment or a walk-in patient. 
* Encode Consultation Summary/ History  
	clinic personnel can input the consultation result from the patient. This includes symptoms, concerns  raised by patients, meds prescribe, diagnosis, services and other info (TBD)
* Patient History   
	List of all consultation or appointments made by the patient
* Consultation List  
	List of all consultation/ appointments by the clinic, this could be listed by date or patients
* Delete   
	Clinic personnel should be able to delete, or cancel previous Consultation saved, they also must indicate the reason why. 
* Edit  
	Clinic personnel should be able to edit or update a previous Consultation saved
* Show Consultation details   
	show the consultation summary/ information

C.   Service Management

* List   
	List of all services offer by clinic
* create new service  
	clinic personnel can create or input a new service
* update service information  
	clinic personnel can edit or update a service information
* get service information  
	show the service information
* delete  
	Clinic personnel should be able to delete, or cancel a  service, they must also indicate the reason why

D.  Patient Management  
Handles the basic information of the patient.

* List  
	List of all patients on the clinic, this should also show the most recent or important information of the patient based on his/ her consultation (compiled) history
* Get patient Information   
	show the basic information of the patient
* Input patient information  
	input the basic information of the patient.
* Update patient information  
	update the basic information of the patient.
* Delete patient  
	delete patient, must indicate reason why

B.  User Management -   
We have 2 kinds of users: User ["Patients"] and Admin [Clinic Personnel]).  a User will always be a Patient, but Patient doesn't necessarily need to be a User (i.e. Walk in) 

* List  
	List of all the system users (patients and clinic personnel).
* Create  
	register a User (clinic personnel only)  to the system. This is separated to the "User" registration by the patients
	assign to role Admin
* Edit  
	update a User (clinic personnel only) Information, 
* Get   
	get the  User (clinic personnel only) Information, 
* Delete  
	Delete the User( clinic personnel only), must indicate reason why
	


Z. Authentication

* Register - assign to role User
* CreateAdmin - assign to role Admin
* Login -
* Current User -
* Forgot Password -
* Change Password
* Update current User Info
* Confirm Register




DEV NOTES 
* we will use  UTC for date and time 
* DELETE Should only be a status "Deleted"
* Aside from UserRoles, we should also have an access restrictions depending on clinic personnel position (TBD)
