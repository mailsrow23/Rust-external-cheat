using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dissector
{
    public enum PFlags
    {
        Unused1 = 1,
        Unused2 = 2,
        IsAdmin = 4,
        ReceivingSnapshot = 8,
        Sleeping = 16,
        Spectating = 32,
        Wounded = 64,
        IsDeveloper = 128,
        Connected = 256,
        VoiceMuted = 512,
        ThirdPersonViewmode = 1024,
        EyesViewmode = 2048,
        ChatMute = 4096,
        NoSprint = 8192,
        Aiming = 16384,
        DisplaySash = 32768,
        Workbench1 = 1048576,
        Workbench2 = 2097152,
        Workbench3 = 4194304
    }

    /// <summary>
    /// ModelState flags
    /// </summary>
    public enum MFlags
    {
        Ducked = 1,
        Jumped = 2,
        OnGround = 4,
        Sleeping = 8,
        Sprinting = 16, // 0x00000010
        OnLadder = 32, // 0x00000020
        Flying = 64, // 0x00000040
        Aiming = 128, // 0x00000080
        Prone = 256, // 0x00000100
        Mounted = 512, // 0x00000200
        Relaxed = 1024, // 0x00000400
    }
}


create name file ("space ")
{

function [A, X] = KKSVD(YTY, sparsity, iterNum)

dimension = size(YTY, 2);
A = eye(dimension);                   %Initialization of the dictionary
%During dictionary learning phase, YTY must be semi positive definite
[Vecs, Vals] = eig(YTY);
if(size(find(Vals < 0), 1) ~= 0)
    for i = 1:size(Vals, 1)
        if(Vals(i, i) < 0)
            Vals(i, i) = 0;
        end
    end
    YTY = Vecs * Vals * Vecs';
end

for iTer = 1:iterNum
    %sparse coding phase
    X = [];
    for i = 1:dimension
        [x, yTY, YTY] = KOMP_ONE(i, YTY(i, :), YTY, A, sparsity);
        X = [X, x];
    end
    %dictionary learning phase
    for i = 1:dimension
        
        xT = X(i, :);
        w = find(xT);
        if ~isempty(w)
            Omega = zeros(dimension, length(w));
            for j = 1:length(w)
                Omega(w(j), j) = 1;
            end
            E_k = eye(dimension) - A * X + A(:, i) * xT;
            E_R = E_k * Omega;
            if E_R == 0
                break;
            end
            [U, S, V] = svd(E_R' * YTY * E_R);
            A(:, i) = E_R * U(:, 1)/sqrt(S(1, 1));
            x_R = sqrt(S(1, 1)) * U(:, 1)';
            X(i, w) = x_R;
        end
    end
end
                            
                            
