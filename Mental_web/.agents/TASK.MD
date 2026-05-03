# Task List: C# WinForms UI Implementation & Navigation

This task list focuses on developing the C# WinForms prototype for the Student Mental Health Appointment Support System (SMHASS). **Database integration is excluded**; mock data and in-memory lists will be used.

## Phase 1: Project Setup (WinForms)
- [x] Initialize C# WinForms Project (.NET 10.0-windows). **(DONE)**
- [x] Define global Theme Class (Colors, Fonts) for easy UI management. **(DONE)**
- [x] Design the `MainForm`: **(DONE)**
    - [x] Set `FormBorderStyle` to `None` for a modern frameless look. **(DONE)**
    - [x] Create a `SidePanel` for navigation. **(DONE)**
    - [x] Create a `ContentPanel` (Main container for switching views). **(DONE)**

## Phase 2: Navigation System
- [x] Implement a navigation logic in `MainForm`. **(DONE)**
- [x] Create `UserControls` for each major screen (Dashboard, Assessment, etc.). **(DONE)**
- [x] Logic to clear `ContentPanel.Controls` and add the selected view. **(DONE)**
- [x] Add smooth hover effects to navigation buttons. **(DONE)**

## Phase 3: Core View Implementation (UserControls)
- [ ] **Dashboard View:**
    - [ ] Circular Progress Bars or Custom Painted Charts for mental health stats.
    - [ ] Card-style Panels for "Next Appointment" and "Recent Tips".
- [ ] **Self-Assessment View:**
    - [ ] Logic to cycle through questions within a single `UserControl`.
    - [ ] Visual feedback for radio buttons/checkboxes (Custom styled).
    - [ ] Result summary page with graphical feedback.
- [ ] **Appointment Scheduling View:**
    - [ ] Custom Calendar control or a grid-based DatePicker.
    - [ ] FlowLayoutPanel for selecting time slots.
    - [ ] Success/Confirmation Dialog (Custom Form).
- [ ] **Resources Hub:**
    - [ ] Scrollable area with image-based resource cards.
    - [ ] Search TextBox with placeholder text.

## Phase 4: Admin View (Prototype)
- [ ] Admin Dashboard (Different sidebar options).
- [ ] DataGridView for Student Management (Styled with modern headers).
- [ ] Appointment Queue management (Approve/Decline buttons).

## Phase 5: UI/UX & Polish
- [ ] Use GDI+ for rounded corners on panels and buttons.
- [ ] Implement a custom "Hover" animation (e.g., changing background color or shifting position).
- [ ] Add a custom "Toast" notification system for alerts.
- [ ] Optimize control rendering (DoubleBuffered = true) to prevent flickering.

## Phase 6: Final Review
- [ ] Verify smooth navigation between all UserControls.
- [ ] Test form resizing and control anchoring/docking.
- [ ] Stress test UI with large amounts of mock text.
