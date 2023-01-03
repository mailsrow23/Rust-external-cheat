using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dissector
{
public enum PlayerFlags
{
    /// <summary>
    /// Unused flag.
    /// </summary>
    Unused1 = 1,

    /// <summary>
    /// Unused flag.
    /// </summary>
    Unused2 = 2,

    /// <summary>
    /// Flag indicating that the player has admin privileges.
    /// </summary>
    IsAdmin = 4,

    /// <summary>
    /// Flag indicating that the player is currently receiving a snapshot.
    /// </summary>
    ReceivingSnapshot = 8,

    /// <summary>
    /// Flag indicating that the player is currently sleeping.
    /// </summary>
    Sleeping = 16,

    /// <summary>
    /// Flag indicating that the player is currently spectating.
    /// </summary>
    Spectating = 32,

    /// <summary>
    /// Flag indicating that the player is currently wounded.
    /// </summary>
    Wounded = 64,

    /// <summary>
    /// Flag indicating that the player is a developer.
    /// </summary>
    IsDeveloper = 128,

    /// <summary>
    /// Flag indicating that the player is currently connected.
    /// </summary>
    Connected = 256,

    /// <summary>
    /// Flag indicating that the player's voice is currently muted.
    /// </summary>
    VoiceMuted = 512,

    /// <summary>
    /// Flag indicating that the player is currently in third-person view mode.
    /// </summary>
    ThirdPersonViewmode = 1024,

    /// <summary>
    /// Flag indicating that the player is currently in eyes view mode.
    /// </summary>
    EyesViewmode = 2048,

    /// <summary>
    /// Flag indicating that the player's chat is currently muted.
    /// </summary>
    ChatMute = 4096,

    /// <summary>
    /// Flag indicating that the player is unable to sprint.
    /// </summary>
    NoSprint = 8192,

    /// <summary>
    /// Flag indicating that the player is currently aiming.
    /// </summary>
    Aiming = 16384,

    /// <summary>
    /// Flag indicating that the player's sash is currently being displayed.
    /// </summary>
    DisplaySash = 32768,

    /// <summary>
    /// Unused flag.
    /// </summary>
    Workbench1 = 1048576,

    /// <summary>
    /// Unused flag.
    /// </summary>
    Workbench2 = 2097152,

    /// <summary>
    /// Unused flag.
    /// </summary>
    Workbench3 = 4194304
}

/// <summary>
/// Model state flags.
/// </summary>
public enum ModelStateFlags
{
    /// <summary>
    /// Flag indicating that the model is currently ducked.
    /// </summary>
    Ducked = 1,

    /// <summary>
    /// Flag indicating that the model has recently jumped.
    /// </summary>
    Jumped = 2,

    /// <summary>
    /// Flag indicating that the model is currently on the ground.
    /// </summary>
    
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
                            
                            
