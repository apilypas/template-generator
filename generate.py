#!/usr/bin/env python3

import os
import shutil
import uuid
import re
import argparse

TEMPLATE_DIR = "template"
OUTPUT_DIR = "output"
OLD_NAME = "TemplateGame"
NEW_NAME = "TestJump"
GUID_RE = re.compile(r"(\{?)([0-9A-Fa-f]{8}(?:-[0-9A-Fa-f]{4}){3}-[0-9A-Fa-f]{12})(\}?)")

def copy_template(project_name, template):
    dest = os.path.join(OUTPUT_DIR, project_name)
    if (os.path.exists(dest)):
        raise FileExistsError(f"Destination {dest} already exists.")
    shutil.copytree(template, dest)
    return dest

def find_all_guids(file_path):
    guids = set()
    with open(file_path, "r", encoding="utf-8") as f:
        content = f.read()
        for m in GUID_RE.finditer(content):
            guids.add(m.group(2).upper())
    return guids

def replace_in_file(file_path, replacements):
    with open(file_path, "r", encoding="utf-8") as f:
        content = f.read()
    for old, new in replacements.items():
        content = content.replace(old, new)
    with open(file_path, "w", encoding="utf-8") as f:
        f.write(content)

def update_directory_names(project_name, dest):
    for root, dirs, _ in os.walk(dest):
        for dname in dirs:
            print(f"Updating directory: {dname}")
            if OLD_NAME in dname:
                os.rename(
                    os.path.join(root, dname),
                    os.path.join(root, dname.replace(OLD_NAME, project_name))
                )

def update_file_names(project_name, dest):
    for root, _, files in os.walk(dest):
        for fname in files:
            if OLD_NAME in fname:
                print(f"Updating file: {fname}")
                os.rename(
                    os.path.join(root, fname),
                    os.path.join(root, fname.replace(OLD_NAME, project_name))
                )

def update_project_name_in_files(project_name, dest):
    replacements = {
        OLD_NAME: project_name
    }
    for root, _, files in os.walk(dest):
        for fname in files:
            if fname.endswith((".cs", ".csproj", ".sln", ".json", ".xml")):
                print(f"Updating name in file: {fname}")
                replace_in_file(os.path.join(root, fname), replacements)

def udate_guids_in_files(dest):
    for root, _, files in os.walk(dest):
        for fname in files:
            if fname.endswith((".sln")):
                print(f"Replacing GUIDs in file: {fname}")
                guids = find_all_guids(os.path.join(root, fname))
                for guid in guids:
                    r = {
                        guid: str(uuid.uuid4()).upper()
                    }
                    replace_in_file(os.path.join(root, fname), r)

def parse_args():
    p = argparse.ArgumentParser(description="Create C# template for Monogame ECS")
    p.add_argument("--template", "-t", help="name of template")
    p.add_argument("--new-name", "-n", help="name of project")
    return p.parse_args()

def update_project(project_name, dest):
    update_directory_names(project_name, dest)
    update_file_names(project_name, dest)
    update_project_name_in_files(project_name, dest)
    udate_guids_in_files(dest)

def main():
    args = parse_args()
    project_name = NEW_NAME
    template = TEMPLATE_DIR
    if args.new_name:
        project_name = args.new_name
    if args.template:
        template = args.template
    print(f"Using new project name: {args.new_name}")
    print(f"Using template: {template}")
    new_path = copy_template(project_name, template)
    update_project(project_name, new_path)
    print(f"New project created at {new_path}")

if __name__ == "__main__":
    main()
