from re import sub
from gtts import gTTS
from glob import glob
from tkinter import Tk
from tkinter import END
from tkinter import WORD
from tkinter import Menu
from tkinter import BOTH
from tkinter import Frame
from tkinter import Entry
from tkinter import Button
from tkinter import StringVar
from tkinter import BooleanVar
from tkinter import messagebox
from tkinter import Checkbutton
from tkinter import scrolledtext
from tkinter import LabelFrame

#NOmbre por defecto de los mp3 generados
NOMBRE_MP3 = "salida"

def generar_audio():
    """Esta funcion genera el audio apartir del texto 
    tomado de el whiget scrolledtext (entrada_texto), cuando se genera
    el audio correctamente se borra el texto ingresado
    (entrada_texto.delete). El try catch lo utilizo para capturar
    las exepciones al no ingresar un texto y al no poder generar el audio"""
    
    try:
        # Tomo el texto desde el scrolledtext (entrada_texto)
        texto = entrada_texto.get("1.0", END)
        # Elimina tabs y saltos de linea
        texto = texto.replace("\n", "").replace("\t", "")
        # Elimina dos o mas espacios en blanco
        texto = sub(r"\s{2,}", " ", texto)
        # Valido que entrada_texto no este vacia
        if validar_entrada(texto):
            # Genero el audio
            tts = gTTS(text=texto, lang="es", slow=check_lento.get())
            # Guardo el audio
            nombre_archivo_final = nombre_archivo(nombre_archivo_salida.get())
            tts.save(nombre_archivo_final)
            # Mensaje de confirmacion de borrado del texto
            respuesta = messagebox.askyesno(title="Exito", 
                                            message=f"Archivo {nombre_archivo_final} generado correctamente.\nBorrar el texto?")
            if respuesta:
                entrada_texto.delete("1.0", END)
        else:
            # Mensaje de error
            messagebox.showinfo(title="Sin texto...", message="No ha ingresado ningun texto.")

    except Exception as e:
        # Lanzo una excepcion
        messagebox.showerror(title="Error", message="Algo paso y no se pudo generar el audio :(")


def pegar_texto(event=None):
    """Esta funcion se encarga de tomar lo que esta en el porta
    papeles (clipboard_get()) e insertarlo en el whidget Text
    (entrada_texto), use un try catch por que al momento de 
    pegar el texto y no habiendo nada en el porta papeles
    arrojaba una exepcion y se colgaba el programa"""

    try:
        # Inserto lo que hay copiado en el porta papeles
        entrada_texto.insert("insert", root.clipboard_get())
    except Exception as e:
        # Si no hay nada capturo la excepecion y salto el error
        pass


def mostrar_menu(event):
    """Esta funcion se encarga de asociar el menú contextual 
    con el widget Scrolledtext (entrada_texto)"""
    # tk_popup se usa para crear un menu en una ubucacion especifica en la pantalla
    # event de tkinter contiene información sobre el evento que ha ocurrido, en este caso el clic del raton.
    # x_root e y_root son la pasicion donde ocurrio ese evento en pantalla.
    menu_pegar.tk_popup(event.x_root, event.y_root)


def validar_entrada(palabra):
    """Esta funcion se encarga de verificar que haya texto para
    generar el audio"""
    # Si el largo de texto es mayor a 0 quiere decir que hay texto
    if len(palabra) > 0:
        return True
    else:
        return False
    
def nombre_archivo(texto):
    """Esta funcion se encarga de verificar si ya hemos creado el archivo
    salida00.mp3, si lo encuantra crea uno nuevo sumandole uno al final
    ejemplo salida01.mp3 va a ser creada si salida00.mp3 esta creada."""
    # verificamos si usamos en nombre por defecto
    if texto == NOMBRE_MP3:
        # Creamos una lista con los resultados de la busqueda
        archivos_mp3 = glob(f"{NOMBRE_MP3}*.mp3")
        # si se encuantran menos de 10 archivos
        if len(archivos_mp3) > 0 and len(archivos_mp3) < 10:
            return NOMBRE_MP3 + "0" + str(len(archivos_mp3)) + ".mp3"
        # si se encuantran mas de 10 archivos
        elif len(archivos_mp3) > 10:
            return NOMBRE_MP3 + str(len(archivos_mp3)) + ".mp3"
        # si no se encuentran archivos
        else:
            return NOMBRE_MP3 + "00.mp3"
    # si no usamos el nombre por defecto retornamos el nuevo nombre
    else:
        return texto + ".mp3"



root = Tk()
# Ancho y largo
root.geometry("400x600")
# Texto de la ventana
root.title("Generar audio")

#### Frames width=ancho height=largo ####

# Frame de texto
frame_texto = LabelFrame(root, text="Texto a convertir en audio", width=400, height=400)
frame_texto.pack(fill=BOTH, expand=True)

# Frame del entry
frame_salid = LabelFrame(root, text="Nombre del archivo mp3 a generar", width=400, height=50)
frame_salid.pack(fill=BOTH)

# Frame del boton
frame_boton = Frame(root, width=300, height=100)
frame_boton.pack(side="left", fill=BOTH, expand=True)

# Frame del check
frame_check = Frame(root, width=100, height=100)
frame_check.pack(side="right", fill=BOTH, expand=True)

# Entrada de texto
entrada_texto = scrolledtext.ScrolledText(frame_texto, state="normal", wrap=WORD)
entrada_texto.pack(padx=10, pady=10, fill=BOTH, expand=True)

# Entrada de texto para la salido de audio
nombre_archivo_salida = StringVar()
nombre_archivo_salida.set(NOMBRE_MP3)
entrada_audio = Entry(frame_salid,text="Lento", textvariable=nombre_archivo_salida)
entrada_audio.pack(padx=10, pady=10, fill=BOTH, expand=True)

# Boton
boton = Button(frame_boton, text="Generar", command=generar_audio)
boton.pack(fill=BOTH, expand=True)

# Check
check_lento = BooleanVar()
check_lento.set(False)
check = Checkbutton(frame_check, text="Lento", variable=check_lento)
check.pack(fill=BOTH, expand=True)

# Click derecho
menu_pegar = Menu(frame_texto, tearoff=0)
menu_pegar.add_command(label="Pegar", command=pegar_texto)
entrada_texto.bind("<Button-3>", mostrar_menu)



root.mainloop()

# Final del camino. xD
