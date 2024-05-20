from gtts import gTTS
from tkinter import Tk
from tkinter import END
from tkinter import Text
from tkinter import Menu
from tkinter import BOTH
from tkinter import Frame
from tkinter import Button
from tkinter import messagebox




def generar_audio():
    """Esta funcion genera el audio apartir del texto 
    tomado de el whiget Text (entrada_texto), cuando se genera
    el audio correctamente se borra el texto ingresado
    (entrada_texto.delete). El try catch lo utilizo para capturar
    las exepciones al no ingresar un texto y al no poder generar el audio"""
    
    try:
        texto = entrada_texto.get("1.0", END)
        tts = gTTS(text=texto, lang="es")
        tts.save("salida.mp3")
        messagebox.showinfo(title="Exito", message="Audio generado correctamente.")
        entrada_texto.delete("1.0", END)

    except:
        messagebox.showerror(title="Error", message="Algo paso y no se pudo generar el audio :(")


def pegar_texto(event=None):
    """Esta funcion se encarga de tomar lo que esta en el porta
    papeles (clipboard_get()) e insertarlo en el whidget Text
    (entrada_texto), use un try catch por que al momento de 
    pegar el texto y no habiendo nada en el porta papeles
    arrojaba una exepcion y se colgaba el programa"""

    try:
        entrada_texto.insert("insert", root.clipboard_get())
    except Exception:
        pass


def mostrar_menu(event):
    """Esta funcion se encarga de asociar el menú contextual 
    con el widget Text (entrada_texto)"""
    menu_pegar.tk_popup(event.x_root, event.y_root)



root = Tk()
# Ancho y largo
root.geometry("400x500")
# Texto de la ventana
root.title("Generar audio")

# Frames width=ancho height=largo
frame_texto = Frame(root, width=400, height=400)
frame_texto.pack(fill=BOTH, expand=True)

frame_boton = Frame(root, width=400, height=100)
frame_boton.pack(fill=BOTH, expand=True)

# Entrada de texto
entrada_texto = Text(frame_texto)
entrada_texto.pack(fill=BOTH, expand=True)

# Click derecho
menu_pegar = Menu(frame_texto, tearoff=0)
menu_pegar.add_command(label="Pegar", command=pegar_texto)
entrada_texto.bind("<Button-3>", mostrar_menu)

# Boton
boton = Button(frame_boton, text="Generar", command=generar_audio)
boton.pack(fill=BOTH, expand=True)

root.mainloop()

# Final del camino. xD