# UI Implementation Strategy (Blazor Web) - PLMUN SMHASS

## Design Philosophy (Web Adaptation)
The Blazor implementation will leverage the full power of **Modern CSS** and **Razor Components** to create a premium, responsive, and "alive" interface. We will focus on a clean, calming aesthetic with smooth transitions and glassmorphic elements.

---

## Visual Design System (CSS Variables)

### 1. Global Styles (`wwwroot/app.css`)
```css
:root {
  --primary: #2D5A27;        /* Deep Sage */
  --secondary: #E8F5E9;      /* Soft Mint */
  --accent: #3F51B5;         /* Individuality Blue */
  --bg: #F8FAF8;
  --surface: rgba(255, 255, 255, 0.8);
  --glass-blur: blur(12px);
  --transition: all 0.3s ease;
}
```

### 2. Aesthetics & Layout
- **Glassmorphism:** Apply `backdrop-filter: var(--glass-blur)` to the sidebar and top navigation for a high-end feel.
- **Responsive Layout:** Use **CSS Grid** for the dashboard cards and **Flexbox** for navigation items.
- **Typography:** Import `Outfit` and `Inter` from Google Fonts.

---

## Core Components (Razor)

### 1. MainLayout.razor
- **Sidebar:** A vertical nav bar that collapses on mobile. Use `@onclick` handlers to toggle state.
- **TopBar:** Displays the current page title and user profile summary.
- **Page Transitions:** Wrap the `@Body` in a `div` with a CSS entry animation (e.g., `slide-up`).

### 2. Assessment Stepper
- Use a C# state machine to track the current question index.
- Animate the change between steps using CSS opacity and transform.

### 3. Dashboard Widgets
- **Mental Health Score:** A circular progress bar implemented using an SVG path with `stroke-dasharray`.
- **Stat Cards:** Use hover effects like `transform: translateY(-5px)` and increased box-shadow.

---

## Interactive Elements (Blazor Interactivity)
- **Render Mode:** Use `InteractiveWebAssembly` for instant feedback on assessments and scheduling.
- **Micro-interactions:** 
  - Buttons should have a subtle "ripple" or color shift on click.
  - Form fields should glow when focused.
  - Notifications (Toasts) should slide in from the top-right.

---

## Technical Guidelines
- **Component Reusability:** Create small components for `Card.razor`, `Button.razor`, and `StatItem.razor`.
- **Responsive Breakpoints:** 
  - Mobile: `< 600px` (Stacked layout).
  - Tablet: `600px - 1024px` (2-column grid).
  - Desktop: `> 1024px` (Sidebar + 3-column grid).
- **Performance:** Minimize the use of heavy libraries; stick to Vanilla CSS for layout and animations.
