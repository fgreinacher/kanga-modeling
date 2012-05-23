/// <reference path="JQuery/jquery-1.5.1.js" />

(function () {

  var delay = (function () {

    var timer = 0;

    return function (callback, ms) {

      clearTimeout(timer);
      timer = setTimeout(callback, ms);

    };

  })();

  jQuery(document).ready(function () {

    var sourceElement = jQuery('#source');
    var styleElement = jQuery('#style');
    var diagramElement = jQuery('#diagram');

    var isFading = false;

    function compile() {

      var arguments = { source: sourceElement.val(), type: 'sequence', style: styleElement.val() };

      kanga.client.createDiagram(arguments, function (result) {

        diagramElement.waitForImages(function () {

          diagramElement.fadeTo('normal', 1.0);

        });
        diagramElement.attr('src', result.diagram);

      });

    }

    sourceElement.bind('input paste', function () {

      if (!isFading) {

        isFading = true;
        diagramElement.fadeTo('slow', 0.25, 'swing', function () { isFading = false });

      }

      delay(function () {

        compile();

      }, 500);

    });

    styleElement.change(function () {

      if (!isFading) {

        isFading = true;
        diagramElement.fadeTo('slow', 0.25, 'swing', function () { isFading = false });

      }

      compile();

    });

    compile();

  });

})();