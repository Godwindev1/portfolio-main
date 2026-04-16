// ─── THEME TOGGLE ───
const html = document.documentElement;
const toggleBtn = document.getElementById('themeToggle');
const toggleLabel = document.getElementById('toggleLabel');

// Respect system preference on first load
const savedTheme = localStorage.getItem('theme');
const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
const initLight = savedTheme === 'light' || (!savedTheme && !prefersDark);
if (initLight) {
  html.classList.add('light');
  toggleLabel.textContent = 'DARK';
}

toggleBtn.addEventListener('click', () => {
  const isLight = html.classList.toggle('light');
  toggleLabel.textContent = isLight ? 'DARK' : 'LIGHT';
  localStorage.setItem('theme', isLight ? 'light' : 'dark');
});

// ─── CUSTOM CURSOR ───
const cursor = document.getElementById('cursor');
const ring = document.getElementById('cursorRing');
let mx = 0, my = 0, rx = 0, ry = 0;

document.addEventListener('mousemove', e => {
  mx = e.clientX; my = e.clientY;
  cursor.style.left = mx + 'px';
  cursor.style.top = my + 'px';
});

function animateRing() {
  rx += (mx - rx) * 0.12;
  ry += (my - ry) * 0.12;
  ring.style.left = rx + 'px';
  ring.style.top = ry + 'px';
  requestAnimationFrame(animateRing);
}
animateRing();

document.querySelectorAll('a, button, .project-card, .domain-card').forEach(el => {
  el.addEventListener('mouseenter', () => {
    cursor.style.width = '14px';
    cursor.style.height = '14px';
    ring.style.width = '48px';
    ring.style.height = '48px';
  });
  el.addEventListener('mouseleave', () => {
    cursor.style.width = '8px';
    cursor.style.height = '8px';
    ring.style.width = '32px';
    ring.style.height = '32px';
  });
});

// ─── SCROLL REVEAL ───
const observer = new IntersectionObserver((entries) => {
  entries.forEach(entry => {
    if (entry.isIntersecting) {
      entry.target.classList.add('visible');
    }
  });
}, { threshold: 0.1 });

document.querySelectorAll('.fade-up').forEach(el => observer.observe(el));



// ─── ACTIVE NAV ON SCROLL ───
const sections = document.querySelectorAll('section[id]');
const navLinks = document.querySelectorAll('.nav-links a');

window.addEventListener('scroll', () => {
  let current = '';
  sections.forEach(s => {
    if (window.scrollY >= s.offsetTop - 100) current = s.id;
  });
  navLinks.forEach(a => {
    a.classList.remove('active');
    if (a.getAttribute('href') === '#' + current) a.classList.add('active');
  });
});


