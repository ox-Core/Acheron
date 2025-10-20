using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using Silk.NET.OpenGL;

namespace Acheron.Engine.Renderer;

public class Shader {
    private uint id;

    GL gl;

    public Shader(GL gl, string vsSrc, string fsSrc) {
        this.gl = gl;

        uint compileStage(GLEnum type, string src) {
            uint shader = gl.CreateShader(type);

            gl.ShaderSource(shader, src);
            gl.CompileShader(shader);

            var success = gl.GetShader(shader, GLEnum.CompileStatus);
            if (success != 0)
                throw new InvalidOperationException("Failed to compile shader " + type + ": " + gl.GetShaderInfoLog(shader));

            return shader;
        }

        uint vs = compileStage(GLEnum.VertexShader, vsSrc);
        uint fs = compileStage(GLEnum.FragmentShader, fsSrc);

        id = gl.CreateProgram();
        gl.AttachShader(id, vs);
        gl.AttachShader(id, fs);
        gl.LinkProgram(id);

        int success = gl.GetProgram(id, GLEnum.LinkStatus);
        if (success != 0)
            throw new InvalidOperationException("Failed to link shader program " + id + ": " + gl.GetProgramInfoLog(id));

        gl.DeleteShader(vs);
        gl.DeleteShader(fs);
    }

    public void Bind() {
        gl.UseProgram(id);
    }

    public void SetUniform(string name, Vector4 value) {
        int location = gl.GetUniformLocation(id, Encoding.ASCII.GetBytes(name));
        if (location != -1) {
            gl.Uniform4(location, value);
        }
    }

    public void SetUniform(string name, bool value) {
        int location = gl.GetUniformLocation(id, Encoding.ASCII.GetBytes(name));
        if (location != -1) {
            gl.Uniform1(location, value ? 1 : 0);
        }
    }

    public void SetUniform(string name, int value) {
        int location = gl.GetUniformLocation(id, Encoding.ASCII.GetBytes(name));
        if (location != -1) {
            gl.Uniform1(location, value);
        }
    }

    public unsafe void SetUniform(string name, Matrix4x4 v) {
        int location = gl.GetUniformLocation(id, Encoding.ASCII.GetBytes(name));
        if (location != -1) {
            double[] matval = [
                v.M11, v.M12, v.M13, v.M14,
                v.M21, v.M22, v.M23, v.M24,
                v.M31, v.M32, v.M33, v.M34,
                v.M41, v.M42, v.M43, v.M44,
            ];
            gl.UniformMatrix4(location, false, matval);
        }
    }

    public bool IsCompiled() => id != 0;
}