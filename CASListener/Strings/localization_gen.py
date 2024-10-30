import tkinter as tk
from tkinter import scrolledtext, simpledialog, messagebox, filedialog
import os
import subprocess
from base64 import b64decode

text_content = ""
save_directory = ""
folder_path = ""  # Initialize folder_path to keep track of the latest directory
open_folder_button = None  # Variable to hold the reference to the "Open Folder" button
has_generated = False  # Flag to track if localized files have been generated

# Function to calculate the FNV-1 64-bit hash
def fnv1_64(data):
    # FNV offset basis for 64-bit
    FNV_OFFSET_BASIS = 0xcbf29ce484222325
    # FNV prime for 64-bit
    FNV_64_PRIME = 0x100000001b3
    
    # Initialize the hash value
    hash_value = FNV_OFFSET_BASIS
    
    # Process each byte of the input data
    for byte in data:
        hash_value = (hash_value * FNV_64_PRIME) & 0xffffffffffffffff
        hash_value ^= byte
    
    return hash_value

# Function to load the content of the selected text file into the text area
def load_text():
    global text_content
    global open_folder_button
    global has_generated
    
    file_path = filedialog.askopenfilename(filetypes=[("Text files", "*.txt")])
    if file_path:
        try:
            with open(file_path, 'r') as file:
                text_content = file.read()  # Save the content to the global variable
                text_area.delete(1.0, tk.END)  # Clear existing text
                text_area.insert(tk.END, text_content)  # Insert the text from the file
                save_button.config(state=tk.NORMAL)  # Enable the save button
                has_generated = False  # Reset the generation flag
                
                # Hide the "Open Folder" button if it exists
                if open_folder_button:
                    open_folder_button.grid_forget()
                    
        except Exception as e:
            text_area.delete(1.0, tk.END)
            text_area.insert(tk.END, f"Failed to open file: {e}")
            save_button.config(state=tk.DISABLED)  # Disable the save button

# Function to save the hashed files based on predefined patterns
def save_hash():
    global save_directory
    global folder_path
    global open_folder_button
    global has_generated

    if has_generated:
        messagebox.showinfo("Info", "Localized files have already been generated. Please open a new base strings file to generate files again.")
        return

    name = simpledialog.askstring("", "Enter something unique for your mod.\nSomething with your name and the mod's name in it is a good choice.")
    if name:
        # Create a lowercase version of the name for hashing
        name_lower = name.lower()
        
        # Calculate the FNV-1 64-bit hash
        name_bytes = name_lower.encode('utf-8')
        hash_value = fnv1_64(name_bytes)
        hash_value_hex = f"{hash_value:016X}"

        # New: Adjust the hash value to remove the first two characters for the base ID
        hash_value_hex_adjusted = hash_value_hex[2:]

        # Ask where to save the files (folder name)
        save_directory = filedialog.askdirectory(title="Select Directory to Save Files")
        if save_directory:
            # Create a directory with the name provided by the user
            folder_path = os.path.join(save_directory, name)
            folder_path = os.path.normpath(folder_path)  # Normalize the path
            os.makedirs(folder_path, exist_ok=True)
            
            # List of filename patterns
            patterns = [
                "S3_220557DA_00000000_16(hash)_(HashedName)Strings_THA_TH%%+STBL.txt",
                "S3_220557DA_00000000_15(hash)_(HashedName)Strings_SWE_SE%%+STBL.txt",
                "S3_220557DA_00000000_14(hash)_(HashedName)Strings_SPA_MX%%+STBL.txt",
                "S3_220557DA_00000000_13(hash)_(HashedName)Strings_SPA_ES%%+STBL.txt",
                "S3_220557DA_00000000_12(hash)_(HashedName)Strings_RUS_RU%%+STBL.txt",
                "S3_220557DA_00000000_11(hash)_(HashedName)Strings_POR_BR%%+STBL.txt",
                "S3_220557DA_00000000_10(hash)_(HashedName)Strings_POR_PT%%+STBL.txt",
                "S3_220557DA_00000000_09(hash)_(HashedName)Strings_EL_GR%%+STBL.txt",
                "S3_220557DA_00000000_08(hash)_(HashedName)Strings_GER_DE%%+STBL.txt",
                "S3_220557DA_00000000_07(hash)_(HashedName)Strings_FRE_FR%%+STBL.txt",
                "S3_220557DA_00000000_06(hash)_(HashedName)Strings_FIN_FI%%+STBL.txt",
                "S3_220557DA_00000000_05(hash)_(HashedName)Strings_DUT_NL%%+STBL.txt",
                "S3_220557DA_00000000_04(hash)_(HashedName)Strings_DAN_DK%%+STBL.txt",
                "S3_220557DA_00000000_03(hash)_(HashedName)Strings_CZE_CZ%%+STBL.txt",
                "S3_220557DA_00000000_02(hash)_(HashedName)Strings_TAI_CN%%+STBL.txt",
                "S3_220557DA_00000000_01(hash)_(HashedName)Strings_CHI_CN%%+STBL.txt",
                "S3_220557DA_00000000_0F(hash)_(HashedName)Strings_POL_PL%%+STBL.txt",
                "S3_220557DA_00000000_0E(hash)_(HashedName)Strings_NOR_NO%%+STBL.txt",
                "S3_220557DA_00000000_0D(hash)_(HashedName)Strings_KOR_KR%%+STBL.txt",
                "S3_220557DA_00000000_0C(hash)_(HashedName)Strings_JPN_JN%%+STBL.txt",
                "S3_220557DA_00000000_0B(hash)_(HashedName)Strings_ITA_IT%%+STBL.txt",
                "S3_220557DA_00000000_0A(hash)_(HashedName)Strings_HUN_HU%%+STBL.txt",
                "S3_220557DA_00000000_00(hash)_(HashedName)Strings_ENG_US%%+STBL.txt"
            ]

            for pattern in patterns:
                # Replace placeholders in the pattern
                file_name = pattern.replace("(hash)", hash_value_hex_adjusted).replace("(HashedName)", name)
                file_path = os.path.join(folder_path, file_name)
                file_path = os.path.normpath(file_path)  # Normalize the file path

                try:
                    # Write content to the file
                    with open(file_path, 'w') as file:
                        file.write(text_content)
                except Exception as e:
                    messagebox.showerror("Error", f"Failed to save file: {file_path}\nError: {e}")

            def open_folder():
                if os.name == 'nt':
                    os.startfile(folder_path)
                elif os.name == 'posix':
                    subprocess.run(['xdg-open', folder_path], check=True)

            messagebox.showinfo(
                "Success",
                f"Files saved in:\n{folder_path}\n\nClick 'Open Folder' to view the files."
            )

            if open_folder_button is None:
                open_folder_button = tk.Button(frame_buttons, text="Open Folder", command=open_folder)
                open_folder_button.grid(row=0, column=2, padx=10, pady=10, sticky="ew")
            else:
                open_folder_button.grid(row=0, column=2, padx=10, pady=10, sticky="ew")  # Show the button if it's already created

            save_button.config(state=tk.DISABLED)
            has_generated = True

# Create the main window
root = tk.Tk()
root.title("Localgen")

# Add a scrollable text area
text_area = scrolledtext.ScrolledText(root, wrap=tk.WORD, width=80, height=20)
text_area.pack(padx=10, pady=10)

# Create a frame to hold buttons and keep their positions consistent
frame_buttons = tk.Frame(root)
frame_buttons.pack(padx=10, pady=10, fill=tk.X)

# Add button to open and load text file
open_button = tk.Button(frame_buttons, text="Open", command=load_text)
open_button.grid(row=0, column=0, padx=10, pady=10, sticky="ew")

# Add button to save hashed files
save_button = tk.Button(frame_buttons, text="Save Hashed Files", command=save_hash, state=tk.DISABLED)
save_button.grid(row=0, column=1, padx=10, pady=10, sticky="ew")

# Start the main event loop
root.mainloop()
