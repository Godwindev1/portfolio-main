
      const hamburger = document.getElementById('navHamburger');
      const LinksContainer  = document.getElementById('navLinks');

      hamburger.addEventListener('click', () => {
        const isOpen = hamburger.classList.toggle('open');
        LinksContainer.classList.toggle('open', isOpen);
        hamburger.setAttribute('aria-expanded', String(isOpen));
      });

      // Close drawer on link click
      LinksContainer.querySelectorAll('a').forEach(link => {
        link.addEventListener('click', () => {
          hamburger.classList.remove('open');
          LinksContainer.classList.remove('open');
          hamburger.setAttribute('aria-expanded', 'false');
        });
      });
    