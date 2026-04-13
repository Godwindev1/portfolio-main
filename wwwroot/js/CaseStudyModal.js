    // ─── STATE ───
    let activeModalId = null;

    // ─── OPEN ───
    function openModal(id) {
        console.log("Opening modal for case study ID:", id);

        // Close any already open modal first
        if (activeModalId !== null) closeModal(activeModalId, false);

        const modal    = document.getElementById('modal-' + id);
        const backdrop = document.getElementById('modalBackdrop');
        const body     = document.getElementById('body-' + id);

        console.log("Modal element:", modal);

        backdrop.classList.add('open');
        modal.classList.add('open');
        document.body.style.overflow = 'hidden';

        // Reset scroll and progress
        body.scrollTop = 0;
        document.getElementById('progress-' + id).style.width = '0%';

        activeModalId = id;
    }

    // ─── CLOSE ───
    function closeModal(id, resetBodyScroll = true) {
        const modal    = document.getElementById('modal-' + id);
        const backdrop = document.getElementById('modalBackdrop');

        modal.classList.remove('open');
        backdrop.classList.remove('open');

        if (resetBodyScroll) document.body.style.overflow = '';

        activeModalId = null;
    }

    function closeActiveModal() {
        if (activeModalId !== null) closeModal(activeModalId);
    }

    // ─── SCROLL PROGRESS ───
    function updateProgress(id) {
        const body = document.getElementById('body-' + id);
        const bar  = document.getElementById('progress-' + id);
        const { scrollTop, scrollHeight, clientHeight } = body;
        const pct = scrollHeight > clientHeight
            ? (scrollTop / (scrollHeight - clientHeight)) * 100
            : 100;
        bar.style.width = pct + '%';
    }

    // ─── KEYBOARD ───
    document.addEventListener('keydown', e => {
        if (e.key === 'Escape' && activeModalId !== null) closeActiveModal();
    });

    // ─── LIGHTBOX ───
    function lightbox(url) {
        if (!url) return;
        const overlay = document.createElement('div');
        overlay.style.cssText = `
            position:fixed; inset:0; z-index:9999;
            background:rgba(0,0,0,0.92);
            display:flex; align-items:center; justify-content:center;
            cursor:zoom-out;
        `;
        const img = document.createElement('img');
        img.src = url;
        img.style.cssText = 'max-width:90vw; max-height:90vh; object-fit:contain;';
        overlay.appendChild(img);
        overlay.addEventListener('click', () => overlay.remove());
        document.body.appendChild(overlay);
    }