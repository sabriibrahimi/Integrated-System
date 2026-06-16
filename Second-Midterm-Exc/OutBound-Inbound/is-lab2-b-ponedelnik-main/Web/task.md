<p id="yui_3_18_1_1_1781436747467_409">At <a href="https://courses.finki.ukim.mk/pluginfile.php/303141/question/questiontext/881366/3/5853594/is-lab3-b-ponedelnik-main.zip">this link</a> the starter code with the solution from the second laboratory exercise is provided.</p>
<p><strong>PART 1: Integration with an external system (30 points)</strong></p>
<p>The task is to integrate with an external system for reviewing consultation comments. The external system exposes the following endpoint:</p>
<ul>
<li><code>GET /api/consultationcomments/byconsultation/{consultationId}/paged</code> — returns paginated results for a specific consultation slot</li>
</ul>
 <p>The full documentation of the questions system is available at <a href="https://integriranisistemi.finki.ukim.mk/docs/index.html">this link</a>.</p>
<p>URL of the comments system: https://integriranisistemi.finki.ukim.mk</p>
 <p>When calling <code>GET /api/consultation/{id}</code> in our application, the response should be enriched with the first 5 comments given for those consultations, fetched from the external system.</p>
 <p>An API key is required to access the external system: <code>gSAOEjaqdZW3MhlJL4miLerblYwlpq9W</code></p>
 <p>The key is sent in the <code>X-Api-Key</code> header on every request.</p>
<p><strong>Note:</strong> Points for this part will be awarded only if the API key is properly stored (via secrets or environment variables).</p>
<p>Results are fetched on-demand from the external system.</p>
<p><strong>Optional (10 points):</strong> To reduce the number of calls, a cache that refreshes every hour must be implemented.</p>
 <p><strong>PART 2: Application security (20 points)</strong></p>
 <p>We want to open our system to external systems, but for security reasons API keys must be used.</p>
 <ul>
<li>An API Key Middleware must be created that authenticates only users with a key issued by you.</li>
</ul>
 <p>To prevent excessive load on the application, a rate limit must be set on at least one endpoint.</p>
 <p><strong>PART 3: Accepting external calls — Inbound REST (30 points)</strong></p>
 <p>Since different faculties use different attendance tracking systems, they want to send data to your system.</p>
<p>For that purpose, access must be enabled through the following endpoints:</p>
<ul>
<li><code>POST /api/external/attendance</code> — accepts a request, validates the basic structure, returns <code>202 Accepted</code> with an ID</li>
<li><code>GET /api/external/attendance/{id}/status</code> — returns the current processing status</li>
</ul>
<p>An InboundAttendanceRequest must be created with the following format:</p>
<p><code class="language-json"><span class="token token">{</span>
    <span class="token token">"userId"</span><span class="token token">:</span>
    <span class="token token">"string"</span><span class="token token">,</span>
    <span class="token token">"consultationId"</span><span class="token token">:</span>
    <span class="token token">"string"</span><span class="token token">,</span>
    <span class="token token">"attendedAt"</span><span class="token token">:</span>
    <span class="token token">"datetime"</span><span class="token token">,</span>
    <span class="token token">"notes"</span><span class="token token">:</span>
    <span class="token token">"string?"</span>
    <span class="token token">}</span></code>
</p>
<p>Only requests sent with a valid key issued by your system should be processed.</p>
<p>Requests received from the external system must be saved in a table InboundAttendanceEntries containing information about:</p>
<ul>
<li>RawPayload: string</li>
<li>Status: ProcessingStatus (Pending/Processing/Completed/Failed)</li>
<li>ApiClientId: Guid</li>
<li>ReceivedAt: DateTime</li>
<li>ProcessedAt: DateTime?</li>
<li>ErrorMessage: string?</li>
<li>CreatedAttendanceId: Guid?</li>
</ul>
<p><strong>Requests waiting to be processed have the status Pending.</strong></p>
<h5>Using a Quartz job, every 30 seconds you must process 5 requests sent from the external system.</h5>