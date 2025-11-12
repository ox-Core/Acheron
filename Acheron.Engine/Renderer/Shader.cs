using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using Acheron.Engine.Types;
using Silk.NET.OpenGL;

namespace Acheron.Engine.Renderer;

public class Shader {
    public uint ID;
    private bool valid;

    readonly GL? gl;

    public static Shader Invalid = new Shader();

    public Shader() {
        valid = false;
    }

    public Shader(GL gl, string vsSrc, string fsSrc) {
        this.gl = gl;

        uint compileStage(GLEnum type, string src) {
            uint shader = gl.CreateShader(type);

            gl.ShaderSource(shader, src);
            gl.CompileShader(shader);

            var success = gl.GetShader(shader, GLEnum.CompileStatus);
            if ((GLEnum)success != GLEnum.True)
                throw new InvalidOperationException("Failed to compile shader " + type + ": " + gl.GetShaderInfoLog(shader));

            return shader;
        }

        uint vs = compileStage(GLEnum.VertexShader, vsSrc);
        uint fs = compileStage(GLEnum.FragmentShader, fsSrc);

        ID = gl.CreateProgram();
        gl.AttachShader(ID, vs);
        gl.AttachShader(ID, fs);
        gl.LinkProgram(ID);

        int success = gl.GetProgram(ID, GLEnum.LinkStatus);
        if ((GLEnum)success != GLEnum.True)
            throw new InvalidOperationException("Failed to link shader program " + ID + ": " + gl.GetProgramInfoLog(ID));

        gl.DeleteShader(vs);
        gl.DeleteShader(fs);

        valid = true;
    }

    public void Bind() {
        gl!.UseProgram(ID);
    }

    public void SetUniform(string name, Vector4 value) {
        int location = gl.GetUniformLocation(ID, Encoding.ASCII.GetBytes(name));
        if (location != -1) {
            gl!.Uniform4(location, value);
        }
    }

    public void SetUniform(string name, bool value) {
        int location = gl.GetUniformLocation(ID, Encoding.ASCII.GetBytes(name));
        if (location != -1) {
            gl!.Uniform1(location, value ? 1 : 0);
        }
    }

    public void SetUniform(string name, int value) {
        int location = gl.GetUniformLocation(ID, Encoding.ASCII.GetBytes(name));
        if (location != -1) {
            gl!.Uniform1(location, value);
        }
    }

    public unsafe void SetUniform(string name, AMatrix4 v) {
        int location = gl.GetUniformLocation(ID, Encoding.ASCII.GetBytes(name));

        if(location != -1) {
            gl!.UniformMatrix4(location, false, v.M);
        }

    }

    public bool IsCompiled() => ID != 0;
    public bool Valid() => valid;
}