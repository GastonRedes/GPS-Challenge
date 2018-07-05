using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;
using Nuclex.UserInterface.Visuals.Flat;
using Nuclex.Input;

using GPS_Challenge.Domain.Model.Entities;


namespace GPS_Challenge.ViewXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /// <summary>Initializes and manages the graphics device used for rendering</summary>
        private GraphicsDeviceManager graphics;
        /// <summary>Batches sprites and text for efficient rendering</summary>
        private SpriteBatch spriteBatch;
        /// <summary>Manages the graphical user interface</summary>
        private GuiManager gui;
        /// <summary>Manages input devices for the game</summary>
        private InputManager input;
        /// <summary>Temporary string builder used for various purposes</summary>
        private StringBuilder tempStringBuilder;
        /// <summary>Contains the text the user has entered on the keyboard</summary>
        private StringBuilder userInputStringBuilder;

        Cuadra cuadra;
        Mapa mapa;
        Vehiculo vehiculo;
        Jugador jugador;
        Juego juego;
        SpriteFont letras;

        List<Type> vehiculos;
        List<Type> obstaculos;
        Type tipo;

        Rectangle posicionImagen;

        Texture2D imagenAlgoRitmosSA;
        Texture2D imagenPresents;
        Texture2D imagenGPSChallenge;

        Texture2D mapa_backgroundIMG;
        
        Texture2D cuadraIMG;
        Texture2D[] imagenes;
        Texture2D[] imagenesVehiculos;
        Texture2D imagenSorpresa;
        Texture2D[] imagenesObstaculos;
        Texture2D imagenLlegada;
        Texture2D lightAura;
        Texture2D fog;
       
        Texture2D imagenGameOver;
        Texture2D imagenJuegoGanado;

        SoundEffect sonidoPresentacion;
        SoundEffect sonidoJuego;
        SoundEffect[] sonidosVehiculos;
        SoundEffect sonidoJuegoGanado;
        SoundEffect sonidoGameOver;

        SoundEffectInstance sonido;
        SoundEffectInstance[] sonidos;

        int tiempo;

        bool musicaPresentacion;
        bool musicaFin;
        
        bool presentacion;
        bool menuUsuarioNuevo;
        bool menuUsuarioExistente;
        bool menuGeneral;
        bool menuDificultad;
        bool menuElegirPartidaGuardada;
        bool menuVehiculo;
        bool menuPuntajes;
        bool enPantallaInicio;
        bool juegoActivo;

        string nombre;

        int tamanioBloque;

        int x;
        int y;

        int posicionVehiculoX;
        int posicionVehiculoY;

        private KeyboardState oldState;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1000;
            Content.RootDirectory = "Content";
            this.input = new InputManager(Services, Window.Handle);
            this.gui = new GuiManager(Services);
            this.tempStringBuilder = new StringBuilder();
            this.userInputStringBuilder = new StringBuilder();

            // Automatically query the input devices once per update
            Components.Add(this.input);

            // You can either add the GUI to the Components collection to have it render
            // automatically, or you can call the GuiManager's Draw() method yourself
            // at the appropriate place if you need more control.
            Components.Add(this.gui);
            this.gui.DrawOrder = 1000;

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            vehiculos = new List<Type>();
            vehiculos.Add(typeof(Auto));
            vehiculos.Add(typeof(Moto));
            vehiculos.Add(typeof(CuatroPorCuatro));

            obstaculos = new List<Type>();
            obstaculos.Add(typeof(ControlPolicial));
            obstaculos.Add(typeof(Piquete));
            obstaculos.Add(typeof(Pozo));

            posicionImagen = new Rectangle(0, 0, 1000, 700);

            imagenes = new Texture2D[3];
            imagenesVehiculos = new Texture2D[3];
            imagenesObstaculos = new Texture2D[3];

            sonidosVehiculos = new SoundEffect[3];
            
            sonidos = new SoundEffectInstance[3];

            tiempo = 0;

            musicaPresentacion = true;
            musicaFin = true;

            presentacion = true;
            enPantallaInicio = false;
            menuUsuarioNuevo = false;
            menuUsuarioExistente = false;
            menuGeneral = false;
            menuDificultad = false;
            menuElegirPartidaGuardada = false;
            menuVehiculo = false;
            juegoActivo = false;
            menuPuntajes = false;

            nombre = "";

            tamanioBloque = 64;

            this.IsMouseVisible = true;
            
            base.Initialize();

            // Create a new screen. Screens manage the state of a GUI and accept input
            // notifications. If you have an in-game computer display where you want
            // to use a GUI, you can create a second screen for that and thus cleanly
            // separate the state of the in-game computer from your game's own GUI
            Viewport viewport = GraphicsDevice.Viewport;
            Screen mainScreen = new Screen(viewport.Width, viewport.Height);
            this.gui.Screen = mainScreen;

            // Each screen has a 'desktop' control. This invisible control by default
            // stretches across the whole screen and serves as the root of the control
            // tree in which all visible controls are managed. All controls are positioned
            // using a system of fractional coordinates and pixel offset coordinates.
            // We now adjust the position of the desktop window to prevent GUI or HUD
            // elements from appearing outside of the title-safe area.
            mainScreen.Desktop.Bounds = new UniRectangle(
            new UniScalar(0.0f, 0.0f), new UniScalar(0.0f, 0.0f), // x and y = 10%
            new UniScalar(0.8f, 0.0f), new UniScalar(0.8f, 0.0f) // width and height = 80%
            );
       
            // Create an instance of the demonstration dialog and add it to the desktop
            // control, which means it will become visible and interactive.
            //mainScreen.Desktop.Children.Add(new DemoDialog());

            // Now let's do something funky: add buttons directly to the desktop.
            // This will also show the effect of the title-safe area.
            //createDesktopControls(mainScreen);
            ////seleccionador de pantallas
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            letras = Content.Load<SpriteFont>("letra");

            imagenAlgoRitmosSA = Content.Load<Texture2D>("AlgoRitmosSA");
            imagenPresents = Content.Load<Texture2D>("Presents");
            imagenGPSChallenge = Content.Load<Texture2D>("GPSChallenge");

            mapa_backgroundIMG = Content.Load<Texture2D>("mapa_backgroundIMG");

            cuadraIMG = Content.Load<Texture2D>("cuadraIMG");
            
            imagenesVehiculos[0] = Content.Load<Texture2D>("vehiculo_autoIMG");
            imagenesVehiculos[1] = Content.Load<Texture2D>("vehiculo_motoIMG");
            imagenesVehiculos[2] = Content.Load<Texture2D>("vehiculo_cuatroXcuatroIMG");
            
            imagenesObstaculos[0] = Content.Load<Texture2D>("obstaculo_policiaIMG");
            imagenesObstaculos[1] = Content.Load<Texture2D>("obstaculo_piqueteIMG");
            imagenesObstaculos[2] = Content.Load<Texture2D>("obstaculo_pozoIMG");

            imagenSorpresa = Content.Load<Texture2D>("sorpresaIMG");

            imagenLlegada = Content.Load<Texture2D>("Llegada");

            lightAura = Content.Load<Texture2D>("lightaura");
            fog = Content.Load<Texture2D>("fog");

            imagenGameOver = Content.Load<Texture2D>("GameOver");
            imagenJuegoGanado = Content.Load<Texture2D>("JuegoGanado");

            sonidoPresentacion = Content.Load<SoundEffect>("Sonidos/Presentacion");
            
            sonidoJuego = Content.Load<SoundEffect>("Sonidos/Juego");

            sonido = sonidoJuego.CreateInstance();

            sonidosVehiculos[0] = Content.Load<SoundEffect>("Sonidos/Auto");
            sonidosVehiculos[1] = Content.Load<SoundEffect>("Sonidos/Moto");
            sonidosVehiculos[2] = Content.Load<SoundEffect>("Sonidos/CuatroPorCuatro");

            sonidos[0] = sonidosVehiculos[0].CreateInstance();
            sonidos[1] = sonidosVehiculos[1].CreateInstance();
            sonidos[2] = sonidosVehiculos[2].CreateInstance();

            sonidoJuegoGanado = Content.Load<SoundEffect>("Sonidos/JuegoGanado");
            sonidoGameOver = Content.Load<SoundEffect>("Sonidos/GameOver");
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (juegoActivo)

                    if (juego.JuegoTermino() == false)
                    {
                        KeyboardState newState = Keyboard.GetState();
                        
                        if ((newState.IsKeyDown(Keys.Up)) && (oldState.IsKeyUp(Keys.Up)))
                        {
                            try
                            {
                                jugador.Vehiculo.MoverVehiculo(Orientacion.Norte);
                                sonidoVehiculo();
                            }
                            catch { }
                        }
                        if ((newState.IsKeyDown(Keys.Down)) && (oldState.IsKeyUp(Keys.Down)))
                        {
                            try
                            {
                                jugador.Vehiculo.MoverVehiculo(Orientacion.Sur);
                                sonidoVehiculo();
                            }
                            catch { }
                        }
                        if ((newState.IsKeyDown(Keys.Right)) && (oldState.IsKeyUp(Keys.Right)))
                        {
                            try
                            {
                                jugador.Vehiculo.MoverVehiculo(Orientacion.Este);
                                sonidoVehiculo();
                            }
                            catch { }
                        }
                        if ((newState.IsKeyDown(Keys.Left)) && (oldState.IsKeyUp(Keys.Left)))
                        {
                            try
                            {
                                jugador.Vehiculo.MoverVehiculo(Orientacion.Oeste);
                                sonidoVehiculo();
                            }
                            catch { }
                        }

                        oldState = newState;
                    }

            // TODO: Add your update logic here
           
           base.Update(gameTime);
        }
        
        /***************************************************/
                      //METODO DRAW CON DIBUJOS 
        /*****************************************************/
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin();

            if (presentacion)
            {
                tiempo += gameTime.ElapsedGameTime.Milliseconds;
                DibujarPresentacion();
            }
           
            if (enPantallaInicio)
                createDesktopControls(this.gui.Screen);
            
            if(menuUsuarioNuevo)
                IngresoNuevoUsuario(this.gui.Screen);

            if(menuUsuarioExistente)
                UsuarioExistente(this.gui.Screen);
           
            if (menuGeneral)
                MenuPartida(this.gui.Screen);

            if (menuElegirPartidaGuardada)
                MenuCargarPartida(this.gui.Screen);

            if (menuPuntajes)
                VerPuntajes(this.gui.Screen);

            if (menuDificultad)
                MenuDificultad(this.gui.Screen);

            if (menuVehiculo)
                MenuVehiculo(this.gui.Screen);

            if (juegoActivo)
            {
                if (juego.JuegoTermino())
                {
                    sonido.Stop();
                    tiempo += gameTime.ElapsedGameTime.Milliseconds;
                    EscribirArchivoPuntajes();
                    DibujarFin();
                }
                else
                {
                    sonido.Play();
                    DibujarMapa();
                    DibujarCarteles();
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        private void DibujarPresentacion() 
        {
            if (tiempo < 4000)

                spriteBatch.Draw(imagenAlgoRitmosSA, posicionImagen, Color.White);

            else
                
                if (tiempo < 5800)
                {
                    spriteBatch.Draw(imagenPresents, posicionImagen, Color.White);

                    if (musicaPresentacion)
                    {
                        sonidoPresentacion.Play();
                        musicaPresentacion = false;
                    }  
                }
                else
                {
                    spriteBatch.Draw(imagenGPSChallenge, posicionImagen, Color.White);

                    if (tiempo > 9800)
                    {
                        tiempo = 0;
                        presentacion = false;
                        enPantallaInicio = true;
                    }
                }
        }

        private void DibujarMapa()
        {
            for (x = 0; x < mapa.GetTamanio(); x++)

                for (y = 0; y < mapa.GetTamanio(); y++)
                {
                    DibujarCuadra();
                    DibujarVehiculo();
                    DibujarSorpresasObstaculos();
                }

            DibujarSombraDeJugador();
            DibujarOscurecerMapa();
            DibujarLlegada();
            
            ButtonControl guardarButton = new ButtonControl();
            guardarButton.Bounds = new UniRectangle(750.0f, 300.0f, 120.0f, 50.0f);
            guardarButton.Text = "Guardar";
            this.gui.Screen.Desktop.Children.Add(guardarButton);

            guardarButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                this.gui.Screen.Desktop.Children.Clear();
                PersistenciaJuego persistidor = new PersistenciaJuego();
                persistidor.persistirJuego(juego);
                juegoActivo = false;
                enPantallaInicio = true;
            };
        }

        private void DibujarCuadra()
        {
            int posX = y * tamanioBloque + 10;
            int posY = x * tamanioBloque + 10;
            int tamanio = mapa.GetTamanio() - 1;
            
            if ((x != tamanio) && (y != tamanio))

                spriteBatch.Draw(cuadraIMG, new Rectangle(posX, posY, tamanioBloque, tamanioBloque), Color.White);
        }

        private void DibujarVehiculo()
        {
            int posX = y * tamanioBloque;
            int posY = x * tamanioBloque;
            tipo = jugador.Vehiculo.Tipo.GetType();

            if (mapa.GetEsquina(x, y) == jugador.Vehiculo.Esquina)
            {
                imagenes = imagenesVehiculos;
                spriteBatch.Draw(Imagen(vehiculos), new Rectangle(posX, posY, 20, 23), Color.White);
                posicionVehiculoX = x;
                posicionVehiculoY = y;
            }
        }

        private void DibujarSorpresasObstaculos()
        {
            cuadra = mapa.GetEsquina(x, y).GetCuadra(Orientacion.Este);
            
            DibujarSorpresaObstaculo(20, 0);

            cuadra = mapa.GetEsquina(x, y).GetCuadra(Orientacion.Sur);

            DibujarSorpresaObstaculo(0, 20);
        }

        private void DibujarSorpresaObstaculo(int xCuadra, int yCuadra)
        {
            int posX = y * tamanioBloque + xCuadra;
            int posY = x * tamanioBloque + yCuadra;

            if (cuadra != null)
            {
                if (cuadra.Obstaculo != null)
                {
                    tipo = cuadra.Obstaculo.GetType();

                    imagenes = imagenesObstaculos;
                    
                    spriteBatch.Draw(Imagen(obstaculos), new Rectangle(posX, posY, 20, 20), Color.White);
                }

                if (cuadra.Sorpresa != null)

                    spriteBatch.Draw(imagenSorpresa, new Rectangle(posX + xCuadra, posY + yCuadra, 20, 20), Color.White);
            }
        }

        private Texture2D Imagen(List<Type> tipos)
        {
            int posicion = 0;

            while (tipo != tipos[posicion])

                posicion++;

            return imagenes[posicion];
        }

        private void DibujarSombraDeJugador()
        {
            int posX = ((int)posicionVehiculoY * tamanioBloque - (lightAura.Width / 2));
            int posY = ((int)posicionVehiculoX * tamanioBloque - (lightAura.Height / 2));

            spriteBatch.Draw(lightAura, new Rectangle(posX, posY, lightAura.Width + 20, lightAura.Height + 20), Color.White);
        }

        private void DibujarOscurecerMapa()
        {
            int posX;
            int posY;

            for (int x = 0; x < mapa.GetTamanio(); x++)

                for (int y = 0; y < mapa.GetTamanio(); y++)
                {
                    posX = y * tamanioBloque + 10;
                    posY = x * tamanioBloque + 10;

                    if (SombraNoCercaDeVehiculo(x, y))

                        spriteBatch.Draw(fog, new Rectangle(posX - 10, posY - 10, tamanioBloque + 25, tamanioBloque + 25), Color.White);
                }
        }

        private bool SombraNoCercaDeVehiculo(int x, int y)
        {
            int esqunaizqsupY = posicionVehiculoY - 2;
            int esqunaizqsupX = posicionVehiculoX - 2;
            int esqunaderinfY = posicionVehiculoY + 1;
            int esqunaderinfX = posicionVehiculoX + 1;

            if (x < esqunaizqsupX) return true;
            if (y < esqunaizqsupY) return true;
            if (x > esqunaderinfX) return true;
            if (y > esqunaderinfY) return true;
            return false;

        }
        
        private void DibujarLlegada()
        {
            int posX = juego.GetLlegadaY() * tamanioBloque;
            int posY = juego.GetLlegadaX() * tamanioBloque;

            spriteBatch.Draw(imagenLlegada, new Rectangle(posX, posY, 25, 20), Color.White);
        }

        private void DibujarCarteles()
        {
            spriteBatch.Draw(mapa_backgroundIMG, new Rectangle(665, 0, 335, 700), Color.White);
            spriteBatch.DrawString(letras, "Jugador: " + jugador.Nombre, new Vector2(670, 20), Color.Black);
            spriteBatch.DrawString(letras, "Modo: " + juego.Modo, new Vector2(670, 60), Color.Black);
            spriteBatch.DrawString(letras, "Vehiculo: " + juego.NombreDelVehiculo(), new Vector2(670, 100), Color.Black);
            spriteBatch.DrawString(letras, "Mov. Limites: " + juego.LimiteDeMovimientos, new Vector2(670, 140), Color.Red);
            spriteBatch.DrawString(letras, "Mov. Actuales: " + jugador.Vehiculo.Movimientos, new Vector2(670, 180), Color.Black);
        }

        private void DibujarFin()
        {
            this.gui.Screen.Desktop.Children.Clear();

            if (juego.JugadorGano())
            {
                spriteBatch.Draw(imagenJuegoGanado, posicionImagen, Color.White);
                spriteBatch.DrawString(letras, "PUNTAJE: " + juego.Puntos(), new Vector2(750, 200), Color.Blue);
                if (musicaFin)
                {
                    sonidoJuegoGanado.Play();
                    musicaFin = false;
                }
            }
            else
            {
                spriteBatch.Draw(imagenGameOver, posicionImagen, Color.White);
                if (musicaFin)
                {
                    sonidoGameOver.Play();
                    musicaFin = false;
                }
            }
            if (tiempo > 5000)
            {
                tiempo = 0;
                musicaFin = true;
                juegoActivo = false;
                enPantallaInicio = true;
            }
        }

        private void sonidoVehiculo()
        {
            int posicion = 0;

            while (jugador.Vehiculo.Tipo.GetType() != vehiculos[posicion])

                posicion++;

            sonidos[posicion].Stop();

            if (!juego.JuegoTermino())

                sonidos[posicion].Play();
        }
        
        private void createDesktopControls(Screen mainScreen)
        {

            // Button to open another "New Game" dialog
            ButtonControl newGameButton = new ButtonControl();
            newGameButton.Text = "Soy Nuevo";
            newGameButton.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth/2 - 100, 250, 200, 50
            );

            newGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                // Insert at index 0 to make it the firstmost window
                //this.gui.Screen.Desktop.Children.Insert(0, new DemoDialog());
                mainScreen.Desktop.Children.Clear();
                this.enPantallaInicio = false;
                this.menuUsuarioNuevo = true;
            };

            mainScreen.Desktop.Children.Add(newGameButton);

            // Button through which the user can quit the application
            ButtonControl quitButton = new ButtonControl();
            quitButton.Text = "Ya tengo usuario";
            quitButton.Bounds = new UniRectangle(
              graphics.PreferredBackBufferWidth / 2 - 200, 350, 400, 50
            );
            quitButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                mainScreen.Desktop.Children.Clear();
                this.enPantallaInicio = false;
                this.menuUsuarioExistente = true;
            };

            mainScreen.Desktop.Children.Add(quitButton);

        }

        private void IngresoNuevoUsuario(Screen mainScreen)
        {
            LabelControl nameEntryLabel = new Nuclex.UserInterface.Controls.LabelControl();
            InputControl nameEntryBox = new Nuclex.UserInterface.Controls.Desktop.InputControl();
            ButtonControl okButton = new Nuclex.UserInterface.Controls.Desktop.ButtonControl();
            WindowControl w = new WindowControl();
            
            //
            // nameEntryLabel
            //
            nameEntryLabel.Text = "Por favor elija su nombre";
            nameEntryLabel.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 75, 200, 150, 50
            );
            mainScreen.Desktop.Children.Add(nameEntryLabel);

            //
            // nameEntryBox
            //
            nameEntryBox.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 150, 280, 300, 30
            );
            mainScreen.Desktop.Children.Add(nameEntryBox);

            //
            //okButton
            //
            okButton.Text = "Guardar";
            okButton.Bounds = new UniRectangle(
              graphics.PreferredBackBufferWidth / 2 - 100, 360, 200, 30 
            );
            
            mainScreen.Desktop.Children.Add(okButton);
            okButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                if ((JugadorExistenteEnArchivo(nameEntryBox.Text)) || (nameEntryBox.Text.Trim() == ""))
                {
                    okButton.Enabled = false;
                    
                    WindowControl window = new WindowControl();
                    window.Title = "Error";

                    LabelControl labelExiste = new LabelControl("El jugador ingresado ya existe o es invalido");
                    labelExiste.Bounds = new UniRectangle(10.0f, 30.0f, 120.0f, 15.0f);

                    ButtonControl aceptarButton = new ButtonControl();
                    aceptarButton.Text = "Aceptar";
                    aceptarButton.Bounds = new UniRectangle(110, 60, 80, 30);

                    window.Bounds = new UniRectangle(graphics.PreferredBackBufferWidth / 2 - 150, 450, 300, 100);
                    window.Children.Add(labelExiste);
                    window.Children.Add(aceptarButton);

                    mainScreen.Desktop.Children.Add(window);

                    aceptarButton.Pressed += delegate(object senderWindow, EventArgs argumentsWindow)
                    {
                        okButton.Enabled = true;
                        window.Close();
                    };
                    
                }
                else
                {
                    nombre = nameEntryBox.Text;
                    StreamWriter sw = new StreamWriter("Jugadores.txt", true);

                    sw.WriteLine(nameEntryBox.Text.Trim());

                    sw.Close();

                    mainScreen.Desktop.Children.Clear();
                    menuUsuarioNuevo = false;
                    menuGeneral = true;
                }
            };
        }

        private bool JugadorExistenteEnArchivo(String nombreUsuario)
        {
            bool jugadorExiste = false;
            StreamReader sr = new StreamReader("Jugadores.txt");
            String line;
            
            while( ((line = sr.ReadLine()) != null) && (jugadorExiste == false) )
            {
                if (line == nombreUsuario)
                    jugadorExiste = true;
            }

            sr.Close();

            return jugadorExiste;
        }

        private void UsuarioExistente(Screen mainScreen)
        {
            StreamReader sr = new StreamReader("Jugadores.txt");
            List<ChoiceControl> usuariosChoice = new List<ChoiceControl>();
            String line;
            int i = 0;

            while ((line = sr.ReadLine()) != null)
            {
                usuariosChoice.Add(new ChoiceControl());
                usuariosChoice[i].Bounds = new UniRectangle(
                    graphics.PreferredBackBufferWidth / 2 - 60, (i + 1) * 30.0f, 120.0f, 16.0f);
                usuariosChoice[i].Text = line;
                
                if (i == 0)
                    usuariosChoice[i].Selected = true;

                mainScreen.Desktop.Children.Add(usuariosChoice[i]);
                i++;
            }
            
            sr.Close();

            ButtonControl aceptarButton = new ButtonControl();
            aceptarButton.Bounds = new UniRectangle(graphics.PreferredBackBufferWidth / 2 - 60, (i + 2) * 30.0f, 120.0f, 50.0f);
            aceptarButton.Text = "Aceptar";
            mainScreen.Desktop.Children.Add(aceptarButton);
            
            if(i == 0)
            {
                LabelControl jugadoresVacioLabel = new LabelControl("No existe ningun usuario");
                jugadoresVacioLabel.Bounds = new UniRectangle(graphics.PreferredBackBufferWidth / 2 - 75, 20, 150, 30);
                mainScreen.Desktop.Children.Add(jugadoresVacioLabel);
            }

            aceptarButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                if (i == 0)
                {
                    mainScreen.Desktop.Children.Clear();
                    menuUsuarioExistente = false;
                    menuUsuarioNuevo = true;
                }
                else
                {
                    i = 0;
                    while (!usuariosChoice[i].Selected)
                        i++;

                    nombre = usuariosChoice[i].Text;

                    mainScreen.Desktop.Children.Clear();
                    menuUsuarioExistente = false;
                    menuGeneral = true;
                }
            };
        }

        private void MenuPartida(Screen mainScreen)
        {
            ButtonControl partidaNuevaButton = new ButtonControl();
            partidaNuevaButton.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 100, 250.0f, 200.0f, 40.0f); 
            partidaNuevaButton.Text = "Nueva Partida";
            this.gui.Screen.Desktop.Children.Add(partidaNuevaButton);

            partidaNuevaButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                mainScreen.Desktop.Children.Clear();
                menuGeneral = false;
                menuVehiculo = true;
            };

            ButtonControl partidaGuardadaButton = new ButtonControl();
            partidaGuardadaButton.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 100, 300.0f, 200.0f, 40.0f);
            partidaGuardadaButton.Text = "Continuar Partida Guardada";
            this.gui.Screen.Desktop.Children.Add(partidaGuardadaButton);

            partidaGuardadaButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                mainScreen.Desktop.Children.Clear();
                menuGeneral = false;
                menuElegirPartidaGuardada = true;
            };

            ButtonControl verPuntajesButton = new ButtonControl();
            verPuntajesButton.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 100, 350.0f, 200.0f, 40.0f);
            verPuntajesButton.Text = "Ver Puntajes";
            this.gui.Screen.Desktop.Children.Add(verPuntajesButton);

            verPuntajesButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                mainScreen.Desktop.Children.Clear();
                menuGeneral = false;
                menuPuntajes = true;
                VerPuntajes(mainScreen);
            };

        }

        private void MenuDificultad(Screen mainScreen)
        {
            ChoiceControl facilChoice = new ChoiceControl();
            ChoiceControl moderadoChoice = new ChoiceControl();
            ChoiceControl dificilChoice = new ChoiceControl();

            facilChoice.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 60 , 275, 120.0f, 16.0f);
            facilChoice.Text = "Facil";
            facilChoice.Selected = true;
            mainScreen.Desktop.Children.Add(facilChoice);
            //
            // moderadoChoice
            //
            moderadoChoice.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 60, 295, 120.0f, 16.0f);
            moderadoChoice.Text = "Moderado";
            mainScreen.Desktop.Children.Add(moderadoChoice);
            //
            // dificilChoice
            //
            dificilChoice.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 60, 315, 120.0f, 16.0f);
            dificilChoice.Text = "Dificil";
            mainScreen.Desktop.Children.Add(dificilChoice);

            ButtonControl aceptarButton = new ButtonControl();
            aceptarButton.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 60, 350, 120.0f, 50.0f);
            aceptarButton.Text = "Jugar";
            mainScreen.Desktop.Children.Add(aceptarButton);

            aceptarButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                if(facilChoice.Selected)
                    juego = new JuegoFacil(jugador);
                else
                    if(moderadoChoice.Selected)
                        juego = new JuegoModerado(jugador);
                    else
                        if(dificilChoice.Selected)
                            juego = new JuegoDificil(jugador);

                mapa = juego.Mapa;
                menuDificultad = false;
                juegoActivo = true;
                mainScreen.Desktop.Children.Clear();
            };
        }

        private void MenuVehiculo(Screen mainScreen)
        {
            ChoiceControl motoChoice = new ChoiceControl();
            ChoiceControl autoChoice = new ChoiceControl();
            ChoiceControl cuatroPorCuatroChoice = new ChoiceControl();

            motoChoice.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 60, 275, 120.0f, 16.0f);
            motoChoice.Text = "Moto";
            motoChoice.Selected = true;
            mainScreen.Desktop.Children.Add(motoChoice);
            //
            // autoChoice
            //
            autoChoice.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 60, 295, 120.0f, 16.0f);
            autoChoice.Text = "Auto";
            mainScreen.Desktop.Children.Add(autoChoice);
            //
            // cuatroPorCuatroChoice
            //
            cuatroPorCuatroChoice.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 60, 315, 120.0f, 16.0f);
            cuatroPorCuatroChoice.Text = "4x4";
            mainScreen.Desktop.Children.Add(cuatroPorCuatroChoice);

            ButtonControl jugarButton = new ButtonControl();
            jugarButton.Bounds = new UniRectangle(
                graphics.PreferredBackBufferWidth / 2 - 60, 350, 120.0f, 50.0f);
            jugarButton.Text = "Aceptar";
            mainScreen.Desktop.Children.Add(jugarButton);
            
            jugarButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                if (motoChoice.Selected)
                    vehiculo = Vehiculo.Moto();
                else
                    if (autoChoice.Selected)
                        vehiculo = Vehiculo.Auto();
                    else
                        if (cuatroPorCuatroChoice.Selected)
                            vehiculo = Vehiculo.CuatroPorCuatro();

                jugador = new Jugador(nombre, vehiculo);
                mainScreen.Desktop.Children.Clear();
                menuVehiculo = false;
                menuDificultad = true;
            };
        }
        
        private void MenuCargarPartida(Screen mainScreen) 
        {
            List<ChoiceControl> jugadoresChoice = new List<ChoiceControl>();
            LabelControl noHayJugador = new LabelControl("No posee partidas guardadas");
            
            if(File.Exists(nombre+".xml"))
            {
                PersistenciaJuego persistidor = new PersistenciaJuego();
                juego = persistidor.cargarJuego(nombre);
                jugador = juego.Jugador;
                mapa = juego.Mapa;
                nombre = juego.Jugador.Nombre;
                vehiculo = juego.Jugador.Vehiculo;
                tipo = juego.Jugador.Vehiculo.Tipo.GetType();
                menuElegirPartidaGuardada = false;
                juegoActivo = true;
                mainScreen.Desktop.Children.Clear();
            }
            else
            {
                noHayJugador.Bounds = new UniRectangle(graphics.PreferredBackBufferWidth / 2 - 75, 300, 150.0f, 16.0f);
                
                ButtonControl aceptarButton = new ButtonControl();
                aceptarButton.Bounds = new UniRectangle(graphics.PreferredBackBufferWidth / 2 - 60, 350, 120.0f, 50.0f);
                aceptarButton.Text = "Aceptar";

                mainScreen.Desktop.Children.Add(noHayJugador);
                mainScreen.Desktop.Children.Add(aceptarButton);

                aceptarButton.Pressed += delegate(object sender, EventArgs arguments)
                {
                    menuElegirPartidaGuardada = false;
                    menuGeneral = true;
                    mainScreen.Desktop.Children.Clear();
                };
            }
        }

        private void VerPuntajes(Screen mainScreen)
        {
            StreamReader sr = new StreamReader("Puntajes.txt");
            String line;
            String puntaje;
            String nombreJugador;
            
            List<LabelControl> puntajesLabel = new List<LabelControl>();
            
            int i = 0;
            while ((line = sr.ReadLine()) != null)
            {
                nombreJugador = line.Split(';')[0];
                puntaje = line.Split(';')[1];

                puntajesLabel.Add(new LabelControl());
                puntajesLabel[i].Bounds = new UniRectangle(graphics.PreferredBackBufferWidth / 2 - 60, ((i + 1) * 30) + 200, 120.0f, 16.0f);
                puntajesLabel[i].Text = nombreJugador + "   " + puntaje;
                mainScreen.Desktop.Children.Add(puntajesLabel[i]);
                i++;
            }

            sr.Close();
            
            ButtonControl aceptarButton = new ButtonControl();
            aceptarButton.Bounds = new UniRectangle(graphics.PreferredBackBufferWidth / 2 - 60, ((i + 2) * 30) + 200, 120.0f, 50.0f);
            aceptarButton.Text = "Aceptar";
            mainScreen.Desktop.Children.Add(aceptarButton);
            
            aceptarButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                menuPuntajes = false;
                menuGeneral = true;
                mainScreen.Desktop.Children.Clear();
            };

        }

        private void EscribirArchivoPuntajes()
        {
            StreamReader sr = new StreamReader("Puntajes.txt");
            String line;
            int puntaje;
            String nombreJugador;

            List<String> jugadores = new List<String>();
            List<int> puntajes = new List<int>();
            List<LabelControl> puntajesLabel = new List<LabelControl>();
            bool ingresoJugador = false;

            while ((line = sr.ReadLine()) != null)
            {
                nombreJugador = line.Split(';')[0];
                puntaje = Convert.ToInt32(line.Split(';')[1]);

                if (!ingresoJugador)
                    if (juego.Puntos() > puntaje)
                    {
                        jugadores.Add(nombre);
                        puntajes.Add(juego.Puntos());
                        ingresoJugador = true;
                    }

                if (nombreJugador == nombre)
                {
                    if (!ingresoJugador)
                    {
                        jugadores.Add(nombreJugador);
                        puntajes.Add(puntaje);
                        ingresoJugador = true;
                    }
                }
                else
                {
                    jugadores.Add(nombreJugador);
                    puntajes.Add(puntaje);
                }

            }

            if (!ingresoJugador)
            {
                jugadores.Add(nombre);
                puntajes.Add(juego.Puntos());
            }

            sr.Close();

            StreamWriter sw = new StreamWriter("Puntajes.txt");

            for (int i = 0; i < jugadores.Count && i<5; i++)
            {
                line = jugadores[i] + ";" + Convert.ToString(puntajes[i]);
                sw.WriteLine(line);
            }

            sw.Close();
        }
    }
}