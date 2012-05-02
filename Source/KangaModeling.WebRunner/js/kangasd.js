function drawDiagrams() {
    var sourceElements = getSourceElements();
    for (i = 0; i < sourceElements.length; i++) {
        convertCodeToDiagram(sourceElements[i]);
    }
}

function convertCodeToDiagram(element) {
    var code = element.childNodes[0].data;
    element.innerHTML = '';
    var img = document.createElement('img');
    img.setAttribute('src', 'http://kangamodeling.org/SequenceDiagram.aspx?code=' + encodeURI(code));
    img.setAttribute('alt', code);
    element.appendChild(img);
}

function getSourceElements() {
    var classname = 'kanga.sd';
    var result = [];
    var els = document.getElementsByTagName('pre');
    for (var i = 0, j = els.length; i < j; i++) {
        if (els[i].className == classname) {
            result.push(els[i]);
        }
    }
    return result;
}

drawDiagrams();