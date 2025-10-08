
// Js cho hiệu ứng đổi màu khi scroll
        window.addEventListener('scroll', function() {
            const navbar = document.querySelector('.main-nav');
            if (window.scrollY > 50) {
                navbar.classList.add('scrolled');
            } else {
                navbar.classList.remove('scrolled');
            }
        });

// Js cho hiệu ứng active khi click vào nút
// Lấy tất cả các nút
function loadEpisode(link, button) {
    var iframe = document.querySelector('.video-player iframe'); // Lấy thẻ iframe
    iframe.src = link; // Thay đổi src của iframe

    // Xóa lớp active khỏi tất cả các nút
    var buttons = document.querySelectorAll('.episode-btn');
    buttons.forEach(function (btn) {
        btn.classList.remove('active');
    });

    // Thêm lớp active vào nút được nhấn
    button.classList.add('active');
}

// xử lý nút share
document.getElementById('btn-share').addEventListener('click', function () {
    var url = encodeURIComponent(window.location.href);
    var fbShareUrl = `https://www.facebook.com/sharer/sharer.php?u=${url}`;
    window.open(fbShareUrl, '_blank', 'width=500,height=300,noopener,noreferrer');
});
