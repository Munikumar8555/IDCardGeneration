window.downloadIdCardPdf = function (elementId, fileName) {
    var element = document.getElementById(elementId);
    if (!element) return;
    html2pdf().set({
        margin: 0.2,
        filename: fileName || 'IDCard.pdf',
        html2canvas: { scale: 2 },
        jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
    }).from(element).save();
};
