function drawDiagrams() {
    var sourceElements = getSourceElements();
    for (i = 0; i < sourceElements.length; i++) {
        convertCodeToDiagram(sourceElements[i]);
    }
}

function convertCodeToDiagram(element) {
    var style = "sketchy";
    if (element.className != null) {
        options = element.className.toLowerCase().split(':');
        if(isOptionSet("classic", options)) style = "classic";
    }
    var code = element.childNodes[0].data;
    element.innerHTML = '';
    var img = document.createElement('img');
    img.setAttribute('src', 'http://kangamodeling.org/SequenceDiagram.aspx?code=' + encodeURI(code) + '&style=' + style);
    img.setAttribute('alt', code);
    element.appendChild(img);
}

function getSourceElements() {
    var kangaSd = 'kanga.sd';
    var result = [];
    var els = document.getElementsByTagName('pre');
    for (var i = 0, j = els.length; i < j; i++) {
        var className = els[i].className;
        if (className == null) continue;
        options = className.split(':');
        if (options.length == 0) continue;
        language = options[0].toLowerCase();
        if (language == kangaSd) {
            result.push(els[i]);
        }
    }
    return result;
}

function isOptionSet(value, list) {
    for (var i = 0; i < list.length; i++)
        if (list[i] == value)
            return true;
    return false;
}

drawDiagrams();