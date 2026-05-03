# UI Implementation Strategy (WinForms) - PLMUN SMHASS

## Design Philosophy (WinForms Adaptation)
Achieving a modern, premium look in C# Windows Forms requires moving beyond default controls. We will use **Custom Painting (GDI+)**, **modern layout techniques**, and **consistent theming** to create a "WOW" factor.

---

## Visual Design System

### 1. Theme Configuration (C# Class)
- **PrimaryColor:** `Color.FromArgb(45, 90, 39)` (Deep Sage)
- **SecondaryColor:** `Color.FromArgb(232, 245, 233)` (Soft Mint)
- **AccentColor:** `Color.FromArgb(63, 81, 181)` (Individuality Blue)
- **SurfaceColor:** `Color.White` with 200 Alpha for semi-transparency.
- **Font:** `new Font("Segoe UI", 12, FontStyle.Regular)` (Native alternative to Outfit/Inter).

### 2. Modern UI Techniques in WinForms
- **Rounded Corners:** Implement a helper method using `GraphicsPath` to clip regions of Panels and Buttons.
- **Frameless Form:** Set `FormBorderStyle = None` and implement a custom title bar panel with `MouseDown`, `MouseMove`, and `MouseUp` events for dragging.
- **Drop Shadows:** Use a custom `OnPaint` override or a background panel with a soft gradient.
- **Anti-Aliasing:** Ensure `e.Graphics.SmoothingMode = SmoothingMode.AntiAlias` is set in all custom paint events.

---

## Component Implementation

### 1. Main Navigation (SidePanel)
- **Dock:** `Left`
- **Control:** Use custom-styled `Buttons` or `Panels` acting as buttons.
- **Active State:** A vertical colored bar (AccentColor) that moves to the active button's side.

### 2. Content Container (ContentPanel)
- **Dock:** `Fill`
- **Navigation Logic:** 
  ```csharp
  public void ShowView(UserControl view) {
      contentPanel.Controls.Clear();
      view.Dock = DockStyle.Fill;
      contentPanel.Controls.Add(view);
  }
  ```

### 3. Glassmorphic Effects
- While true glassmorphism is difficult in standard WinForms, we can simulate it using:
  - Semi-transparent background colors on panels.
  - Layering panels over high-quality background images.
  - Using high-quality PNG icons with transparency.

---

## Interactive Elements (Micro-animations)
- **Hover Transitions:** Use a `Timer` to gradually change a button's `BackColor` or `Padding` to create a smooth transition effect.
- **Entry Animations:** Slide `UserControls` into the `ContentPanel` by adjusting their `Left` property in a loop or Timer.

---

## Technical Guidelines
- **Double Buffering:** Set `this.DoubleBuffered = true` and use `SetStyle(ControlStyles.OptimizedDoubleBuffer, true)` on all custom controls to eliminate flicker.
- **Anchoring & Docking:** Use `Anchor` (Top, Left, Right) to ensure the UI stays consistent when the window is resized.
- **DPI Awareness:** Ensure the application is set to `PerMonitorV2` in `app.manifest` for crisp text on high-resolution screens.
