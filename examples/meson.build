examples = {
  'health': 'ecs/health.cpp',
  'stages': 'ecs/stages.cpp',
  'fps_counter': 'ecs/fps_counter.cpp',
  'events': 'ecs/events.cpp',
  'functions_as_systems': 'ecs/functions_as_systems.cpp',
  'module': 'ecs/module.cpp',
  'window_config': 'window/window_config.cpp',
  'rainbow': 'renderer/rainbow.cpp'
}

link_args = []
if host_machine.system() == 'windows'
    link_args += ['-lstdc++exp']
    link_args += ['-lgdi32', '-lkernel32', '-lshell32', '-luser32']
endif

acheron_dep = dependency('acheron')

foreach name, src : examples
    executable(
        name,
        src,
        dependencies: [acheron_dep],
        include_directories: [include_directories('../src')],
        link_args: link_args,
        override_options : ['cpp_std=c++23']
    )
endforeach
