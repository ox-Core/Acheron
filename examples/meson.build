examples = {
  'hello': 'basic/hello.cpp'
}

link_args = []
if host_machine.system() == 'windows'
    link_args += ['-lstdc++exp']
endif

acheron_dep = dependency('acheron')

foreach name, src : examples
    executable(
        name,
        src,
        dependencies: [acheron_dep],
        link_args: link_args,
        override_options : ['cpp_std=c++23']
    )
endforeach
