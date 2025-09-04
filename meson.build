project('acheron', 'cpp')

srcs = [
    'src/acheron.cpp',
    'src/ecs/component.cpp',
    'src/ecs/entity.cpp',
    'src/ecs/system.cpp',
    'src/ecs/world.cpp',
    'src/modules/window/window.cpp',
]

include = [
    include_directories('src')
]

cc = meson.get_compiler('cpp')

glfw_dep = dependency('glfw3', static : true)

m_dep = cc.find_library('m', required : false)
gdi32_dep = cc.find_library('gdi32', required : false)
winmm_dep = cc.find_library('winmm', required : false)

link_args = []
if host_machine.system() == 'windows'
    link_args += ['-lstdc++exp']
endif

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
    dependencies: dependencies,
    link_args: link_args,
    override_options : ['cpp_std=c++23']
)

acheron_dep = declare_dependency(
    link_with: acheron_lib,
    include_directories: include,
    dependencies: dependencies
)

meson.override_dependency('acheron', acheron_dep)

subdir('examples')
