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
- [x] **Dashboard View:** **(DONE)**
    - [x] Circular Progress Bars or Custom Painted Charts for mental health stats. **(DONE)**
    - [x] Card-style Panels for "Next Appointment" and "Recent Tips". **(DONE)**
- [x] **Self-Assessment View:** **(DONE)**
    - [x] Logic to cycle through questions within a single `UserControl`. **(DONE)**
    - [x] Visual feedback for radio buttons/checkboxes (Custom styled). **(DONE)**
    - [x] Result summary page with graphical feedback. **(DONE)**
- [x] **Appointment Scheduling View:** **(DONE)**
    - [x] Custom Calendar control or a grid-based DatePicker. **(DONE)**
    - [x] FlowLayoutPanel for selecting time slots. **(DONE)**
    - [x] Success/Confirmation Dialog (Custom Form). **(DONE)**
- [x] **Resources Hub:** **(DONE)**
    - [x] Scrollable area with image-based resource cards. **(DONE)**
    - [x] Search TextBox with placeholder text. **(DONE)**

- [x] **Phase 4: Admin View (Prototype)** **(DONE)**
    - [x] Admin Dashboard (Different sidebar options). **(DONE)**
    - [x] DataGridView for Student Management (Styled with modern headers). **(DONE)**
    - [x] Appointment Queue management (Approve/Decline buttons). **(DONE)**

## Phase 5: UI/UX & Polish
- [ ] Use GDI+ for rounded corners on panels and buttons.
- [ ] Implement a custom "Hover" animation (e.g., changing background color or shifting position).
- [ ] Add a custom "Toast" notification system for alerts.
- [ ] Optimize control rendering (DoubleBuffered = true) to prevent flickering.

## Phase 6: Final Review
- [ ] Verify smooth navigation between all UserControls.
- [ ] Test form resizing and control anchoring/docking.
- [ ] Stress test UI with large amounts of mock text.
