# Task List: Blazor Web UI Implementation & Navigation

This task list focuses on developing the Blazor Web prototype for the Student Mental Health Appointment Support System (SMHASS). **Database integration is excluded**; mock services and in-memory lists will be used.

## Phase 1: Project Setup (Blazor)
- [ ] Initialize Blazor Web App (Interactive WebAssembly).
- [ ] Set up global CSS variables in `wwwroot/app.css` (Colors, Typography).
- [ ] Configure the `MainLayout.razor`:
    - [ ] Modern Sidebar navigation (Deep Sage theme).
    - [ ] Responsive header with User Profile and Logo.
    - [ ] Main content area with smooth transitions.

## Phase 2: Navigation & Component System
- [ ] Define the `NavMenu.razor` component with custom CSS for active states.
- [ ] Create a `PageTitle` service or use the built-in one for dynamic browser titles.
- [ ] Implement a "Slide-in" animation for page content changes.

## Phase 3: Core Page Implementation (Razor Components)
- [ ] **Dashboard (`Index.razor`):**
    - [ ] Flexbox/Grid layout for stats cards.
    - [ ] Progress charts (using CSS or a simple SVG component).
    - [ ] "Recent Activity" list.
- [ ] **Self-Assessment (`Assessment.razor`):**
    - [ ] Multi-step form logic in C#.
    - [ ] Question cards with fade-in animations.
    - [ ] Results summary view with color-coded feedback.
- [ ] **Appointment Scheduling (`Scheduling.razor`):**
    - [ ] Custom Calendar component (HTML/CSS/C#).
    - [ ] Slot selection grid with "available/selected" states.
    - [ ] Mock "Booking Success" modal.
- [ ] **Resources Hub (`Resources.razor`):**
    - [ ] Card grid for mental health articles.
    - [ ] Category filtering logic in C#.

## Phase 4: Admin Interface (Prototype)
- [ ] Admin layout with restricted navigation.
- [ ] Student management table with search and filter.
- [ ] Appointment approval dashboard.

## Phase 5: UI/UX & Polish
- [ ] Implement Glassmorphism effects using CSS `backdrop-filter`.
- [ ] Add micro-animations (hover effects on buttons and cards).
- [ ] Ensure full responsiveness for mobile and tablet views.
- [ ] Optimize load times for WASM assets.

## Phase 6: Final Review
- [ ] Run `dotnet watch` to verify hot reload and UI consistency.
- [ ] Test navigation flow through all core features.
- [ ] Final accessibility check (Alt text, Contrast, Aria labels).
