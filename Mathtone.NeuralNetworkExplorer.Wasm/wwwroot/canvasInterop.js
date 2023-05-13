// canvasInterop.js

window.canvasInterop = {
   drawBitmap: function (canvasId, base64Image) {
      const canvas = document.getElementById(canvasId);
      const context = canvas.getContext('2d');

      // Clear the canvas
      context.clearRect(0, 0, canvas.width, canvas.height);

      // Create an image object
      const image = new Image();
      image.src = "data:image/png;base64," + base64Image;

      // Draw the image onto the canvas
      image.onload = function () {
         context.drawImage(image, 0, 0);
      };
   }
};