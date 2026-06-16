<p>At the link you are given a starter code with the solution from the second laboratory exercise.</p>
<p>It is required to implement one of the following tasks:</p>
<ol id="yui_3_18_1_1_1781013173946_416">
<li>A scheduled job that will delete Attendance records where the student has not confirmed attendance (Status = Absent) and the associated Attendance was held more than 7 days ago. It is necessary to ensure execution of this job at an interval of 3 minute. The implementation of the job should be done using BackgroundService (50 points).</li>
<li id="yui_3_18_1_1_1781013173946_415">ETL integration with an external SQLite database (RoomDirectory → Room, ConsultationSlots → Consultation) where the data will be synchronized every 5 minutes using a Quartz Job (100 points). The external database is available at the <a href="https://courses.finki.ukim.mk/pluginfile.php/303141/question/questiontext/881366/2/1644247/review.db?time=1776836300137">link</a>.<br><br>Access credentials for the external database:<br><br>
<p>Host: db-eftim.finki.ukim.mk</p>
<p>Port: 1433</p>
<p>Authentication: Username &amp; Password</p>
<p>Username: db_student</p>
<p>Password: db_exams2023!!*</p>
<p>Database: ISLegacyDb</p>
<br><br>You need to map the keys and the other attributes as per the table below:
<table style="width: 54.7884%; height: 335px;" border="1" cellspacing="0" cellpadding="8">
<thead>
<tr style="height: 65px;">
<th style="width: 22.8047%;">External Table</th>
<th style="width: 14.5478%;">Column</th>
<th style="width: 4.19397%;">→</th>
<th style="width: 16.3827%;">Entity</th>
<th style="width: 42.0708%;">Attribute</th>
</tr>
</thead>
<tbody>
<tr style="height: 41px;">
<td style="width: 22.8047%;">RoomDirectory</td>
<td style="width: 14.5478%;">RoomName</td>
<td style="width: 4.19397%;">→</td>
<td style="width: 16.3827%;">Room</td>
<td style="width: 42.0708%;">Name</td>
</tr>
<tr style="height: 41px;">
<td style="width: 22.8047%;">RoomDirectory</td>
<td style="width: 14.5478%;">MaxCapacity</td>
<td style="width: 4.19397%;">→</td>
<td style="width: 16.3827%;">Room</td>
<td style="width: 42.0708%;">Capacity</td>
</tr>
<tr style="height: 41px;">
<td style="width: 22.8047%;">ConsultationSlots</td>
<td style="width: 14.5478%;">SlotStart</td>
<td style="width: 4.19397%;">→</td>
<td style="width: 16.3827%;">Consultation</td>
<td style="width: 42.0708%;">StartTime</td>
</tr>
<tr style="height: 41px;">
<td style="width: 22.8047%;">ConsultationSlots</td>
<td style="width: 14.5478%;">SlotEnd</td>
<td style="width: 4.19397%;">→</td>
<td style="width: 16.3827%;">Consultation</td>
<td style="width: 42.0708%;">EndTime</td>
</tr>
<tr style="height: 65px;">
<td style="width: 22.8047%;">ConsultationSlots</td>
<td style="width: 14.5478%;">RoomCode</td>
<td style="width: 4.19397%;">→</td>
<td style="width: 16.3827%;">Consultation</td>
<td style="width: 42.0708%;">RoomId</td>
</tr>
</tbody>
</table>
</li>
</ol>

<p>DB Schema</p>
<p>CREATE TABLE RoomDirectory (<br>&nbsp; &nbsp; RoomCode &nbsp; &nbsp;INT &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; IDENTITY(1,1) NOT NULL,<br>&nbsp; &nbsp; RoomName &nbsp; &nbsp;NVARCHAR(200) NOT NULL,<br>&nbsp; &nbsp; MaxCapacity INT &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; NOT NULL DEFAULT 30,<br>&nbsp; &nbsp; IsActive &nbsp; &nbsp;BIT &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; NOT NULL DEFAULT 1,<br>&nbsp; &nbsp; CreatedAt &nbsp; DATETIME2 &nbsp; &nbsp; NOT NULL DEFAULT GETUTCDATE(),<br>&nbsp; &nbsp; UpdatedAt &nbsp; DATETIME2 &nbsp; &nbsp; NOT NULL DEFAULT GETUTCDATE(),<br>&nbsp; &nbsp; CONSTRAINT PK_RoomDirectory PRIMARY KEY (RoomCode)<br>);<br>&nbsp;<br>CREATE TABLE ConsultationSlots (<br>&nbsp; &nbsp; SlotId &nbsp; &nbsp;INT &nbsp; &nbsp; &nbsp; IDENTITY(1,1) NOT NULL,<br>&nbsp; &nbsp; SlotStart DATETIME2 NOT NULL,<br>&nbsp; &nbsp; SlotEnd &nbsp; DATETIME2 NOT NULL,<br>&nbsp; &nbsp; RoomCode &nbsp;INT &nbsp; &nbsp; &nbsp; NOT NULL,<br>&nbsp; &nbsp; CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),<br>&nbsp; &nbsp; UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),<br>&nbsp; &nbsp; CONSTRAINT PK_ConsultationSlots PRIMARY KEY (SlotId),<br>&nbsp; &nbsp; CONSTRAINT FK_ConsultationSlot_Room FOREIGN KEY (RoomCode)<br>&nbsp; &nbsp; &nbsp; &nbsp; REFERENCES RoomDirectory (RoomCode)<br>);</p>