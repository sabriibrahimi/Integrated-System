
<p>At the link you are given a starter code.</p>
<p>It is required to implement one of the following tasks:</p>
<ol id="yui_3_18_1_1_1780997464366_354">
<li id="yui_3_18_1_1_1780997464366_437">Scheduled job that will delete Booking records for tourists who have canceled their trip (Status = Cancelled), and refers to bookings made more than 7 days ago. It is necessary to ensure the execution of this job at an interval of 3 minutes. The implementation of the job should be using BackgroundService (50 points).</li>
<li id="yui_3_18_1_1_1780997464366_353">ETL integration with an external database (ToursDirectory → Tours, TourOfferings → Offers) where the data will be synchronized every 5 minutes with Quartz (100 points).<br><br>Access credentials for the external database:<br>
<pre id="yui_3_18_1_1_1780997464366_352">Server=db-eftim.finki.ukim.mk\SQL2022EXPRESS,1433;User Id=db_student;Password=db_exams2023!!*;Database=ISLegacyDb;MultipleActiveResultSets=true;Persist Security Info=False;Encrypt=False;Trusted_Connection=False;Integrated Security=False;</pre>
<br>
<p>Host: db-eftim.finki.ukim.mk</p>
<p>Port: 1433</p>
<p>Authentication: Username &amp; Password</p>
<p>Username: db_student</p>
<p>Password: db_exams2023!!*</p>
<p>Database: ISLegacyDb</p>
<br>You need to map the keys and the other attributes as per the table below:
<table style="width: 54.7884%; height: 414.878px;" border="1" cellspacing="0" cellpadding="8">
<thead>
<tr style="height: 65px;">
<th style="width: 30.8118%;">External Table</th>
<th style="width: 22.13%;">Column</th>
<th style="width: 5.27732%;">→</th>
<th style="width: 19.7466%;">Entity</th>
<th style="width: 22.13%;">Attribute</th>
</tr>
</thead>
<tbody>
<tr style="height: 62.6215px;">
<td style="width: 30.8118%;">ToursDirectory</td>
<td style="width: 22.13%;">Name</td>
<td style="width: 5.27732%;">→</td>
<td style="width: 19.7466%;">Tour</td>
<td style="width: 22.13%;">Name</td>
</tr>
<tr style="height: 62.6215px;">
<td style="width: 30.8118%;">ToursDirectory</td>
<td style="width: 22.13%;">Capacity</td>
<td style="width: 5.27732%;">→</td>
<td style="width: 19.7466%;">Tour</td>
<td style="width: 22.13%;">Capacity</td>
</tr>
<tr style="height: 62.6215px;">
<td style="width: 30.8118%;">TourOfferings</td>
<td style="width: 22.13%;">AgencyName</td>
<td style="width: 5.27732%;">→</td>
<td style="width: 19.7466%;">Offers</td>
<td style="width: 22.13%;">AgencyId</td>
</tr>
<tr style="height: 62.6215px;">
<td style="width: 30.8118%;">TourOfferings</td>
<td style="width: 22.13%;">TourName</td>
<td style="width: 5.27732%;">→</td>
<td style="width: 19.7466%;">Offers</td>
<td style="width: 22.13%;">TourId</td>
</tr>
</tbody>
</table>
</li>
</ol>

<div>
<div>CREATE TABLE ToursDirectory (</div>
<div>Name VARCHAR(255) PRIMARY KEY,</div>
<div>Capacity INT NOT NULL</div>
<div>);</div>
<br>
<div>CREATE TABLE TourOfferings (</div>
<div>AgencyName VARCHAR(255) NOT NULL,</div>
<div>TourName VARCHAR(255) NOT NULL,</div>
<div>PRIMARY KEY (AgencyName, TourName),</div>
<div>FOREIGN KEY (TourName) REFERENCES ToursDirectory(Name)</div>
<div>);</div>
</div>