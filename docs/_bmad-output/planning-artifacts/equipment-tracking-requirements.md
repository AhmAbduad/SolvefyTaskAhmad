# Equipment Tracking System — Requirements and Delivery Guide

**Status:** Draft for owner review  
**Source:** Mr. Tariq's voice note and follow-up email  
**Purpose:** Define a small, easy-to-use system that shows who has equipment, when it is due back, and whether it is ready for the next job.

## 1. Recommendation

Build a mobile-first equipment checkout and return system with a simple office dashboard. A team member should be able to record a checkout or return in a few seconds; Saima and Mr. Tariq should be able to see current availability without calling around.

Start with a focused MVP: an equipment register, named checkouts, due dates and reminders, approval for the drone, damage reporting, pooled stock counts, and searchable history. Prefer WhatsApp for reminders because the team already uses it, but confirm the available WhatsApp integration and cost before making it a release dependency. The core system must still work if WhatsApp delivery is unavailable.

## 2. Problem and desired outcomes

Today, equipment is taken informally and its location is not reliably visible. This has led to a second GPS rover being purchased while the original was sitting in a vehicle, equipment being kept longer than needed, office staff fielding status calls, and damaged equipment being discovered only at the next job.

The system should:

- Show who currently has each individually tracked item and when it is due back.
- Make checkout and return quick enough for field staff to use consistently.
- Make the availability of pooled equipment visible as a quantity.
- Prevent the drone from being checked out without Hamza's approval.
- Prompt timely returns and record damage before equipment is made available again.
- Let office staff answer “where is it?” and let the owner review what was out last month.

## 3. Users and responsibilities

| User | Needs and responsibilities |
|---|---|
| Team member / borrower | Find available equipment, check it out, see the due date, report a return or damage. |
| Saima / office staff | See current availability, borrower, due date, overdue items, and history; help maintain the equipment register. |
| Hamza / drone approver | Approve or decline drone checkout requests. |
| Mr. Tariq / owner | See the overall equipment position and historical activity; decide later whether the new rover needs approval. |

The system should support ordinary team borrowing without imposing approval steps on every item. The drone is the only confirmed approval-controlled item.

## 4. Scope

### MVP — in scope

1. A register of individually tracked equipment and its current status.
2. A register of pooled equipment quantities, initially **4 tripods** and **6 field tablets**.
3. Checkout and return records tied to a named team member.
4. Expected return date/time, overdue status, and reminders.
5. Hamza's approval before a drone checkout is confirmed.
6. Return-condition reporting; damaged items are flagged and not shown as available until cleared.
7. A current-status dashboard usable by the office and owner.
8. Searchable, date-filterable history, including a report for last month.
9. A mobile-friendly, low-friction interface for field staff.

### Not assumed for the MVP

- GPS or live location tracking.
- Purchasing, depreciation, or maintenance-cost accounting.
- Approvals for equipment other than the drone.
- A particular vendor, technology stack, or WhatsApp provider.
- Automated WhatsApp integration until feasibility and cost are confirmed.

## 5. Functional requirements

| ID | Requirement | Priority |
|---|---|---|
| FR-01 | The system shall maintain a register of individually tracked equipment, including a distinct identity, display name, and current status for each item. | Must |
| FR-02 | The system shall allow an authorized user to record a checkout against a named borrower and record the checkout time and expected return time. | Must |
| FR-03 | A user shall be able to check out a complete survey kit or an individual component. The system shall prevent a component assigned to a checked-out kit from also appearing available for separate checkout. | Must |
| FR-04 | The system shall show, for each item, whether it is available, awaiting approval, checked out, overdue, damaged / under repair, or retired. | Must |
| FR-05 | The system shall track pooled stock by quantity rather than requiring a separate asset record for every tripod or tablet. It shall prevent checkout of more units than are available. | Must |
| FR-06 | The pooled stock register shall start with 4 tripods and 6 field tablets. Authorized users shall be able to adjust counts, with the change recorded in the history. | Must |
| FR-07 | A drone checkout shall remain unconfirmed until Hamza approves it. The system shall record the request, decision, approver, and decision time. | Must |
| FR-08 | On return, the borrower or office user shall record whether the item is returned in working condition or damaged. A damaged item shall be flagged and withheld from available stock until an authorized user clears it. | Must |
| FR-09 | The system shall remind borrowers before an item is due and when it becomes overdue. Reminders shall identify the equipment and due date and provide a clear next action. | Must |
| FR-10 | Saima and Mr. Tariq shall be able to see current availability, borrower, due date, outstanding approvals, overdue equipment, and damage flags in one dashboard. | Must |
| FR-11 | An authorized user shall be able to review and filter checkout, return, approval, damage, and stock-adjustment history by date, borrower, and equipment. | Must |
| FR-12 | The system shall support a report for a selected past period, including last month, showing what was checked out, who had it, and checkout / return dates. | Must |
| FR-13 | The field checkout and return flows shall be designed for mobile use and require only the minimum information necessary to complete the action. | Must |
| FR-14 | The owner shall be able to mark selected equipment as requiring approval in future, without requiring approval for all equipment. | Should |

## 6. User workflows

### Check out available equipment

1. Borrower opens the equipment list on a phone.
2. Borrower chooses an available item, kit, or number of pooled units.
3. Borrower confirms their identity and expected return date/time.
4. System records the checkout and updates availability immediately.
5. System sends or schedules due reminders.

**Drone exception:** The system creates a pending request and notifies Hamza. The drone remains unavailable to other borrowers; the checkout becomes active only after approval. If declined, the request is closed and the drone remains available.

### Return equipment

1. Borrower or office user selects the active checkout and records the return.
2. They confirm working condition or report damage.
3. A working item becomes available; a damaged item is flagged and withheld.
4. The system records the return time and condition and updates the dashboard.

### Find equipment and review history

Saima or Mr. Tariq filters the current list by status or searches by item / borrower. For a retrospective, they select the required date range (including last month) and review checkout and return events with borrower names.

## 7. Acceptance criteria

The MVP is ready for field use when all of the following are demonstrated:

1. **Fast checkout:** A signed-in borrower can check out a normally available item on a phone in **10 seconds or less** in a usability test, excluding sign-in time. This is a proposed measurable interpretation of “a few seconds” and should be confirmed with Mr. Tariq.
2. **Accountability:** Every active checkout of an individually tracked item displays the borrower, item, checkout time, and due time.
3. **Kit integrity:** Checking out a kit makes its included components unavailable individually until the kit is returned or its contents are explicitly reconciled.
4. **Pooled counts:** With 4 tripods recorded, the system permits up to 4 concurrent tripod checkouts and rejects an additional checkout until one is returned.
5. **Drone approval:** A drone request cannot become a confirmed checkout without Hamza's approval, and the request and decision remain visible in history.
6. **Damage handling:** A returned damaged item appears as damaged / unavailable, is visible to office staff, and cannot be checked out until cleared.
7. **Reminders:** A borrower receives a due reminder and an overdue reminder through the agreed channel, or the system clearly shows a delivery failure for follow-up.
8. **Office visibility:** Saima can identify an item's borrower and due date from the dashboard without contacting field staff.
9. **Historical report:** A user can select last month and see the equipment, borrower, checkout date, and return date (or that it remained outstanding at period end).
10. **Traceability:** Stock changes, approvals, checkouts, returns, and damage reports retain who made the change and when.

## 8. Suggested delivery sequence

1. **Confirm the open decisions below** and assign a person to maintain the equipment register.
2. **Prepare the initial inventory:** identify individually tracked items, define survey-kit contents, and enter pooled quantities.
3. **Deliver the core register and checkout / return flows**, with borrower identity, due dates, item status, and office dashboard.
4. **Add approval and damage handling** before piloting the drone workflow and return checks.
5. **Add reminders and historical reporting**, then test actual WhatsApp delivery if feasible.
6. **Pilot with a small group of field staff and Saima.** Measure checkout time, missing records, overdue returns, and reported damage. Simplify any step that users regularly skip.
7. **Roll out to the full team** after reconciling the initial inventory and explaining the quick checkout / return process.

## 9. Decisions to confirm with Mr. Tariq

| Decision | Proposed starting point | Why it matters |
|---|---|---|
| Does the new rover need approval? | No approval in MVP until the owner decides; make the setting configurable. | The email says this is undecided. |
| When are reminders sent, and to whom? | Borrower reminder before due time and at overdue time; office view shows overdue items. Confirm timing and any escalation. | Exact cadence and escalation were not specified. |
| Reminder delivery channel | Prefer WhatsApp if an approved integration is feasible; otherwise use an agreed channel and keep in-app status visible. | The team already uses WhatsApp, but no provider, cost, or consent details were given. |
| How should borrowers identify themselves? | Use a quick signed-in session or another one-step identity method; do not rely on unverified free-text names. | Accountability depends on reliable borrower identity, but the team's device and sign-in practices are unknown. |
| Survey kit contents | Record each kit's components and whether they may ever be checked out separately. | Kit and component availability must not conflict. |
| Who can maintain records? | Allow Saima / designated office staff to manage inventory and clear damage flags. | The owner identified Saima as the office point of contact; permission boundaries need confirmation. |
| Inventory identification | Assign a clear label / QR code to individually tracked high-value items where practical. | Can reduce search and checkout effort; exact tagging approach is a delivery choice. |

## 10. Success measures to track after rollout

Establish a baseline during the pilot, then review monthly:

- Share of active individually tracked checkouts with a named borrower and due date.
- Number of overdue checkouts and average time overdue.
- Number of equipment-location enquiries handled manually by the office.
- Number of damage incidents reported only after the next checkout.
- Time needed to complete a normal checkout and return.
- Accuracy of pooled counts during periodic stock checks.

No numeric improvement targets are inferred from the source notes; Mr. Tariq should set targets after a short baseline period.
