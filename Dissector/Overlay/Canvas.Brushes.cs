using SharpDX.Direct2D1;

namespace Dissector.Overlay
{
    /// <summary>
    /// Brushes for our canvas
    /// </summary>
    public partial class Canvas d
    { 
        private static SolidColorBrush _metalBrush;
        private static SolidColorBrush _stoneBrush;
        private static SolidColorBrush _sulfurBrush;
        private static SolidColorBrush _hempBrush;
        private static SolidColorBrush _sleeperBrush;
        private static SolidColorBrush _woodenCrateBrush;
        private static SolidColorBrush _militaryCrateBrush;
        private static SolidColorBrush _lootBrush;
        private static SolidColorBrush _playerBrush;
        private static SolidColorBrush _barrelBrush;
        private static SolidColorBrush _sheetMetalDoorBrush;
        private static SolidColorBrush _doubleMetalDoorBrush;
        private static SolidColorBrush _garageDoorBrush;
        private static SolidColorBrush _armoredDoorBrush;
        private static SolidColorBrush _toolCupboardBrush;
        private static SolidColorBrush _crosshairBrush;
        private static SolidColorBrush _animalBrush;

        /// <summary>
        /// Initializes our drawing brushes/other resources
        /// </summary>
        private static void InitializeBrushes()
        {
            _metalBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.DarkSlateBlue);
            _stoneBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Silver);
            _sulfurBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Yellow);
            _hempBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Green);
            _animalBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Purple);
            _sleeperBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Red);
            _woodenCrateBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Blue);
            _militaryCrateBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.LimeGreen);
            _lootBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.DarkOrange);
            _playerBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.MediumAquamarine);
            _barrelBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Blue);
            _sheetMetalDoorBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.OrangeRed);
            _doubleMetalDoorBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Pink); 
            _garageDoorBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Blue);
            _armoredDoorBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Blue);
            _toolCupboardBrush = new SolidColorBrush(_renderTarget, SharpDX.Color.Blue);
            _crosshairBrush = new SolidColorBrush(_renderTarget, new SharpDX.Mathematics.Interop.RawColor4(255,0,0,0));
        }


        private static void DisposeBrushes()
        {
            _metalBrush.Dispose();
            _stoneBrush.Dispose();
            _sulfurBrush.Dispose();
            _hempBrush.Dispose();
            _sleeperBrush.Dispose();
            _woodenCrateBrush.Dispose();
            _militaryCrateBrush.Dispose();
            _lootBrush.Dispose();
            _playerBrush.Dispose();
            _barrelBrush.Dispose();
            _sheetMetalDoorBrush.Dispose();
            _doubleMetalDoorBrush.Dispose();
            _garageDoorBrush.Dispose();
            _armoredDoorBrush.Dispose();
            _toolCupboardBrush.Dispose();
            _crosshairBrush.Dispose();
        }

        /// <summary>
        /// Allow user to modify brush color for sulfur
        /// </summary>
        /// <param name="color"></param>
        public static void SetSulfurBrush(System.Drawing.Color color)
        {
            _sulfurBrush.Color = new SharpDX.Color(color.R, color.G, color.B, color.A);
        }
    }
    return false; 
}

var body = document.body,
    overlay = document.querySelector('.overlay'),
    overlayBtts = document.querySelectorAll('button[class$="overlay"]'),
    openingBtt;
    
[].forEach.call(overlayBtts, function(btt) {

  btt.addEventListener('click', function() { 
     
     /* Detect the button class name */
     var overlayOpen = this.className === 'open-overlay';
     
     /* storing a reference to the opening button */
     if (overlayOpen) {
        openingBtt = this;
     }
     
     /* Toggle the aria-hidden state on the overlay and the 
        no-scroll class on the body */
     overlay.setAttribute('aria-hidden', !overlayOpen);
     body.classList.toggle('noscroll', overlayOpen);
     
     /* On some mobile browser when the overlay was previously
        opened and scrolled, if you open it again it doesn't 
        reset its scrollTop property */
     overlay.scrollTop = 0;
     
      /* forcing focus for Assistive technologies but note:
    - if your modal has just a phrase and a button move the
      focus on the button
    - if your modal has a long text inside (e.g. a privacy
      policy) move the focus on the first heading inside 
      the modal
    - otherwise just focus the modal.

    When you close the overlay restore the focus on the 
    button that opened the modal.
    */
    if (overlayOpen) {
       overlay.focus();
    }
    else {
       openingBtt.focus();
       openingBtt = null;
    }

  }, false);

});

document.addEventListener('keyup', (ev) => {
    if (ev.key === "Escape") {
        const overlay = document.querySelector('.overlay');
        if (overlay && !overlay.hasAttribute('aria-hidden')) {
            overlay.setAttribute('aria-hidden', 'true');
            document.body.classList.remove('noscroll');
            if (openingBtt) {
                openingBtt.focus();
                openingBtt = null;
            }
        }
    }
});

