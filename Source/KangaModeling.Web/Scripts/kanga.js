/// <reference path="jquery-1.5.1.js" />

kangaApi = function (apiBaseUri) {

  var _apiBaseUri = apiBaseUri;

  this.createDiagram = function (text, type, style, callback) {

    $.getJSON(_apiBaseUri + '/create', { text: encodeURI(text), type: type, style: style }, function (result) {

      callback(result);

    });
  }

  this.replaceElementsByDiagrams = function () {

    var sourceElements = getSourceElements();
    for (i = 0; i < sourceElements.length; i++) {

      convertCodeToDiagram(sourceElements[i]);

    }

  }

  var convertCodeToDiagram = function (element) {

    var text = element.childNodes[0].data;

    createDiagram(text, 'sequence', 'sketchy', function (result) {

      element.innerHTML = '';
      var img = document.createElement('img');
      img.setAttribute('src', result.uri);
      img.setAttribute('alt', text);
      element.appendChild(img);

    });

  }

  var getSourceElements = function () {

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
}

var KANGA = new kangaApi('/api');