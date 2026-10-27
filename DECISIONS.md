\# DECISIONS.md


\---



\## D-01: Made Equipment Module that displays identity name and current status for each item

\*\*The gap\*\*
The requirement does not clear to show whether to delete the equipment or edit the equipment and also did not mention to apply assigned to functionality in the add equipment module.

\*\*What I decided\*\*
I chose to show the equipment module and also with add functionality and fixed the assigned to hardcoded it in the backend to ahmad means that by default ahmad will be assigned.

\*\*The alternative\*\*
I chose to show edit and view functionality only to the frontend without implementing it in the backend dotnet and i can also implement assigned to any other person in the list without
hard coded it.

\*\*Why this one\*\*
I have chosen this because the requirement did not mention it and only to show these functionality in the frontend and only one person is responsible for assigned to ahmad or saima like 
mentioned in requirements.
 
\*\*What would change my mind\*\*
If the requirement mention to apply edit and delete the equipment i will implement it and if the assigned to is not hardcoded i will add the functionality in both frontend and backend.


\---



\## D-02: Implemented the checkout functionality

\*\*The gap\*\*
I have not shown the time the equipment received .

\*\*What I decided\*\*
 I shows equipment list, assigned to , date without time and purpose  what is the purpose to use this equipment.

\*\*The alternative\*\*
I added the purpose to use this equipment because it was not shown on requirements. 

\*\*Why this one\*\*
It completely shows the equipment, assign to , return date and the purpose which is most important because to show the purpose to use this .

\*\*What would change my mind\*\*
If the requirement mention to show time and show the assign to because it is hardcoded at the current stage.


\---


\## D-03: Implemented the checkin functionality

\*\*The gap\*\*
I have shows all the equipments if the equipment is already taken i have not implemented it.

\*\*What I decided\*\*
I decided to show all the equipments with its return date and time and show condition and notes which is optional.

\*\*The alternative\*\*
To not show the equipment which is not available.

\*\*Why this one\*\*
It completely shows the checkin functionality like equipment, return date and time and condition. 

\*\*What would change my mind\*\*
If the requirement mention to not show the equipment which is already taken then not show it.



\---

\## D-04: Implement the drone approval functionality only hamza can approve it.

\*\*The gap\*\*
It only shows the latest drone who has requested it.

\*\*What I decided\*\*
I decided to show latest drone request if approved or not and only one request because the drone can be taken only by one person.

\*\*The alternative\*\*
To show all the requests for drone approval.

\*\*Why this one\*\*
It completely shows the drone approval by hamza and show the latest drone request because drone can be taken only by one person.

\*\*What would change my mind\*\*
It is irrelavent to show all the drone request  because it can be taken only by one person.


\---

\## D-05: Implement the My Equipment Functionality 

\*\*The gap\*\*
This feature does not show the equipment to return or not by the admin only by the user.

\*\*What I decided\*\*
I decided to implement this functionality because if the user want to return the equipment without admin.

\*\*The alternative\*\*
Both can use this functionality like user and admin.

\*\*Why this one\*\*
It only shows the latest return request and to return it or not.

\*\*What would change my mind\*\*
If requirement mention to show all the equipment then implement it right now it is only showing one.



\---

\## D-06: Implement the damage report functionality

\*\*The gap\*\*
It only implement the add functionality of the item that is damage or not 

\*\*What I decided\*\*
I decided to make a separate module for this because if the equipment is damaged then report it.

\*\*The alternative\*\*
The checkin functionality also shows the damage item but i decided to implement the separate page for this.

\*\*Why this one\*\*
It only shows the damage report and the status of it.

\*\*What would change my mind\*\*
If the requirement also mention to change the status of the item then i can also implement it.




\---

\## D-07: Implement the Equipment History page that shows the latest activity like checkout , checin , damage etc.

\*\*The gap\*\*
It only shows the latest activity and does not show the equipment when added.

\*\*What I decided\*\*
I decided to make a separate module for this that shows the latest activity.

\*\*The alternative\*\*
Also show which equipment is added.

\*\*Why this one\*\*
I have implemented this to see all the latest activity in our application.

\*\*What would change my mind\*\*
If the requirement also mention to show the equipment added i will also implement it

\---

\## D-08: Implement the team members functionality

\*\*The gap\*\*
It stores the team members but does not show the team members in the equipment assigning.

\*\*What I decided\*\*
I decided to make a separate page for team members.

\*\*The alternative\*\*
Also shows the team members in equipment page but i have not implemented it.

\*\*Why this one\*\*
To make a separate page for this to add the team members like 25 as mentioned in the requirements.

\*\*What would change my mind\*\*
Also show the team members in equipment page.

\---

