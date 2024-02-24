# dwsh - dameware shell for Windows

## Overview

dwsh is a command shell for Dameware Mini Remote Control. It provides the ability to manage connections from the command line, a logging feature with time tracking and the ability to create Dameware Connection files which can be launched from Windows Explorer.

![dwsh](https://github.com/dmaccormac/dwsh/blob/main/img/cap.png)

## Download

Download the latest release [here](https://github.com/dmaccormac/dwsh/releases/tag/dwsh).

## Usage

dwsh can either be run as a portable executable or installed to the system. To enable the Dameware Connection file assocation dwsh must be installed.

## Installation

Launch `dwsh.exe` and run the `install` command (requires administrative privileges).

    dwsh: install

Install performs the following actions:

- Create registry entries for Dameware Connection `.dwc` file assocation.
- Copy `dwsh.exe` to the Dameware installation directory.
- Add Dameware installation directory to the user `PATH` environment variable.

When no arguments are provided to the `install` command, dwsh will attempt to locate the Dameware installation directory. Optionally, you can specify the Dameware installation directory as a parameter.

## Dameware connection files

Dameware connection files are plaintext files with the `.dwc` filename extension which contain the name or IP address of a single host. They can be used as a convenient way of managing and launching connections from Windows Explorer.

## Logging

The log file `dwsh.log` is located in the current user's home directory.
