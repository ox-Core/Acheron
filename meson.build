project('acheron', 'cpp')

srcs = [
    'src/acheron.cpp',
    'src/ecs/component.cpp',
    'src/ecs/entity.cpp',
    'src/ecs/system.cpp',
    'src/ecs/world.cpp',
    'src/modules/window/window.cpp',
    'src/modules/renderer/renderer.cpp',
    'src/modules/renderer/quad.cpp',
    'src/modules/renderer/renderers/mesh2d.cpp',
]

include = [
    include_directories('src')
]

cc = meson.get_compiler('cpp')

glfw_proj = subproject('glfw')
glfw_dep = glfw_proj.get_variable('glfw_dep')
glfw_lib = glfw_proj.get_variable('glfw_lib')

m_dep = cc.find_library('m', required : false)
gdi32_dep = cc.find_library('gdi32', required : false)
winmm_dep = cc.find_library('winmm', required : false)

link_args = []
if host_machine.system() == 'windows'
    link_args += ['-lstdc++exp']
endif

cpp_args = ['-static-libgcc', '-static-libstdc++']

dependencies = [
    m_dep,
    gdi32_dep,
    winmm_dep,
    glfw_dep,
]

acheron_lib = library(
    meson.project_name(),
    srcs,
    include_directories: include,
    link_with: [glfw_lib],
    dependencies: dependencies,
    link_args: link_args,
    cpp_args: cpp_args,
    override_options : ['cpp_std=c++23']
)

acheron_dep = declare_dependency(
    link_with: acheron_lib,
    include_directories: include,
    dependencies: dependencies
)

meson.override_dependency('acheron', acheron_dep)

subdir('examples')
