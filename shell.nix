{ pkgs ? import <nixpkgs> {} }:
  pkgs.mkShell {
    nativeBuildInputs = with pkgs.buildPackages; [
      libgcc
      meson
      cmake
      ninja
      xorg.libX11
      xorg.libXrandr
      xorg.libXinerama
      xorg.libXcursor
      xorg.libXi
      xorg.libxcb
      xorg.xeyes
      glfw
      libGL
      gdb
      valgrind
      doxygen
      doxygen_gui
      live-server
    ];
    shellHook = ''
      export LD_LIBRARY_PATH=${pkgs.xorg.libX11}/lib:${pkgs.libGL}/lib:${pkgs.glfw}/lib:$LD_LIBRARY_PATH
    '';

    hardeningDisable = [ "fortify" ];
}
