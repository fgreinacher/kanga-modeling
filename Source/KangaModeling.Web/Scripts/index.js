/// <reference path="Jquery/jquery-1.5.1.js" />

(function (kanga, $) {

  var delay = (function () {

    var timer = 0;

    return function (callback, ms) {

      clearTimeout(timer);
      timer = setTimeout(callback, ms);

    };

  })();

  $(document).ready(function () {

    var sourceElement = $('#source');
    sourceElement.linedtextarea();
    
    var styleElement = $('#style');
    
    var diagramElement = $('#diagram');
    
    var errorsElement = $('#errors');

    var isFading = false;

    function compile() {

      var arguments = { source: sourceElement.val(), type: 'sequence', style: styleElement.val() };

      kanga.client.createDiagram(arguments, function (result) {
      
        diagramElement
          .attr('src', result.diagram)
          .attr('width', result.diagramWidth)
          .attr('height', result.diagramHeight);

        diagramElement.waitForImages(function () {

          diagramElement.fadeTo('normal', 1.0);

        });
        
        $(errorsElement).empty();

        var items = [];

        $.each(result.errors, function (i, error) {

          items.push('<li>Line ' + error.token.line + ': ' + error.message + '</li>');

        });

        if ($.isEmptyObject(items)) {

          items.push('<li>None</li>');

        }

        $(errorsElement).append(items.join(''));

      });

    }

    sourceElement.bind('input paste', function () {

      if (!isFading) {

        isFading = true;
        diagramElement.fadeTo('slow', 0.25, 'swing', function () { isFading = false; });

      }

      delay(function () {

        compile();

      }, 500);

    });

    styleElement.change(function () {

      if (!isFading) {

        isFading = true;
        diagramElement.fadeTo('slow', 0.25, 'swing', function () { isFading = false; });

      }

      compile();

    });

    compile();

  });

})(kanga, jQuery);